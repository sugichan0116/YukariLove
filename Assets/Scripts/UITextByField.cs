using UnityEngine;
using UniRx;
using System.Linq;
using TMPro;
using System.Reflection;

[RequireComponent(typeof(UIField))]
public class UITextByField : MonoBehaviour
{
    public string prefix, suffix;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<UIField>().onChanged
            .Subscribe(value => {
                GetComponent<TextMeshProUGUI>().text = $"{prefix}{value}{suffix}";
            });
    }
}
