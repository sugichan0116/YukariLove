using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(
  fileName = "CardData",
  menuName = "ScriptableObject/CardData",
  order = 0)
]
[System.Serializable]
public class CardData : ScriptableObject
{
    public enum CardType
    {
        FOOD,
        GAME,
        BOOK
    }

    public string product;
    public Sprite icon;
    public CardType type;
    public int love;
    public int price;
    public CardEffect effect;
    
}
