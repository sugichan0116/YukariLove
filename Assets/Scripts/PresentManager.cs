using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using System.Linq;

public class PresentManager : MonoBehaviour
{
    public Window present;
    public GameObject cardCase;
    public GameObject resultCardCase;

    public void AwakePresentWindow()
    {
        present.gameObject.SetActive(true);
        
        //reset
        foreach (Transform child in resultCardCase.transform)
        {
            Destroy(child.gameObject);
        }

        //show result
        var selectedCards = cardCase.GetComponentsInChildren<CardHolder>()
            .Where(holder => holder.GetComponentInChildren<Card>().selected);

        foreach(var holder in selectedCards)
        {
            var clone = Instantiate(holder);
            var func = clone.GetComponentInChildren<CardBehaviour>();
            Destroy(func.cover);
            Destroy(func);
            Destroy(clone.GetComponentInChildren<Card>());
            Destroy(clone.GetComponentInChildren<CardUI>());
            clone.transform.SetParent(resultCardCase.transform, false);
        }
    }
}
