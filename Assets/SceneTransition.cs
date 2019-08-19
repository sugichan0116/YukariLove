using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;
using System;
using Hellmade.Sound;

[RequireComponent(typeof(ObservableEventTrigger))]
public class SceneTransition : MonoBehaviour
{
    public SceneObject scene;

    // Start is called before the first frame update
    void Start()
    {
        var trigger = GetComponent<ObservableEventTrigger>();


        trigger
            .OnPointerClickAsObservable()
            .Take(1)
            .Subscribe(_ =>
            {
                Debug.Log($"@@@ {trigger}");
                EazySoundManager.StopAll();
                SceneManager.LoadScene(scene);
            })
            .AddTo(this);
        
    }
}
