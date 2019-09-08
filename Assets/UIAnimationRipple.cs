using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class UIAnimationRipple : MonoBehaviour
{
    public float diameter = 4f;
    public float time = 0.4f;
    private Sequence sequence;
    private Image ripple;
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        ripple = GetComponent<Image>();
        color = ripple.color;
    }

    public void OnClicked()
    {
        ripple.transform.localScale = Vector3.zero;
        ripple.transform.position = Input.mousePosition;
        sequence = DOTween.Sequence()
            .Append(ripple.transform.DOScale(Vector3.one * diameter, time))
            .Join(DOVirtual.Float(1, 0, time, value => {
                ripple.color = new Color(color.r, color.g, color.b, color.a * value);
            }))
            .OnComplete(() => {
                ripple.color = color;
                ripple.transform.localScale = Vector3.zero;
            });
    }

    private void OnDestroy()
    {
        sequence.Kill();
    }
}
