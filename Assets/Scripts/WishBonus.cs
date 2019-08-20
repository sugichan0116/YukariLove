using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using System;

public class WishBonus : MonoBehaviour
{
    [Serializable]
    public class WishEffect
    {
        public Sprite icon;
        public CardEffect effect;
    }

    //public Sprite[] sprites;
    //public CardEffect[] effects;
    public WishEffect[] wishes;
    private int index;

    [Range(0, 1)]
    public float probability;

    [Header("UI")]
    public GameObject content;
    public TextMeshProUGUI text;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        var cardlist = FindObjectOfType<CardList>();
        
        cardlist
            .onUpdateEffect
            .Where(_ => index >= 0)
            .Subscribe(_ =>
            {
                cardlist.PushEffect(wishes[index].effect);
            })
            .AddTo(this);

        this
            .ObserveEveryValueChanged(wb => wb.index)
            .Subscribe(index => {
                content.gameObject.SetActive(index >= 0);

                if (index < 0) return;
                text.text = $"{wishes[index].effect}";
                image.sprite = wishes[index].icon;
            });

        var random = new System.Random();

        FindObjectOfType<Player>()
            .ObserveEveryValueChanged(p => p.day)
            .Where(day => day % 7 == 0)
            .Subscribe(_ =>
            {
                index = (UnityEngine.Random.value <= probability) 
                    ? random.Next(wishes.Length) : -1;
            })
            .AddTo(this);
    }
}
