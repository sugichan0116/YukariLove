using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using Hellmade.Sound;

public class LoveBehaviour : MonoBehaviour
{
    public ParticleSystem particle;
    public AudioClip sfx;

    // Start is called before the first frame update
    void Start()
    {
        var player = GetComponent<Player>();

        player
            .ObserveEveryValueChanged(p => p.level)
            .Where(level => level > 1)
            .Subscribe(level =>
            {
                particle.Play();
                EazySoundManager.PlayUISound(sfx);
            });
    }
}
