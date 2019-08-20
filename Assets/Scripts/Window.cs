using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.Events;

public class Window : MonoBehaviour
{
    public Subject<Unit> onClose = new Subject<Unit>();
    public UnityEvent onWindowOpen = new UnityEvent();
    public UnityEvent onWindowClose = new UnityEvent();

    [Header("Close Area")]
    public ObservableEventTrigger outRegion;

    // Start is called before the first frame update
    void Start()
    {
        outRegion
            .OnPointerClickAsObservable()
            .Subscribe(_ => {
                onClose.OnNext(Unit.Default);
                InactiveWindow();
            })
            .AddTo(this);
    }
    
    public void ActiveWindow()
    {
        gameObject.SetActive(true);
        onWindowOpen.Invoke();
    }

    public void InactiveWindow()
    {
        onWindowClose.Invoke();
        gameObject.SetActive(false);
    }
}
