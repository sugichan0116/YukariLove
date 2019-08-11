using UnityEngine;
using UniRx;
using System;

[RequireComponent(typeof(UIField))]
public class UIGaugeByField : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var e = GetComponent<GaugeElement>();

        GetComponent<UIField>().onChanged
            .Subscribe(value => {
                if(value is ValueType v)
                {
                    var i = float.Parse(v.ToString());
                    e.volume = i;
                }
            });
    }
}
