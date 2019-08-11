using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;

public class Message : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        player
            .ObserveEveryValueChanged(p => p.message)
            .Subscribe(m => {
                text.text = m;
            });
    }
}
