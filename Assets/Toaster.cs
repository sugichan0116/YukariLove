using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using System.Linq;
using System;

[RequireComponent(typeof(UIField))]
public class Toaster : MonoBehaviour
{
    public enum Type
    {
        FLOAT,
        BOOL
    }

    public enum TriggerType {
        EQUAL,
        GREATER,
        LESS
    }

    public Type type;
    public TriggerType trigger;
    public float threshold;
    public Toast toast;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<UIField>()
            .onChanged
            .Subscribe(v =>
            {
                if(type == Type.BOOL)
                {
                    toast.gameObject.SetActive(Convert.ToBoolean(v));
                }
                if(type == Type.FLOAT)
                {
                    toast.gameObject.SetActive(Judge(Convert.ToSingle(v), threshold));
                }
                
            });
    }

    public bool Judge(float a, float b)
    {
        switch(trigger)
        {
            case TriggerType.EQUAL:
                return a == b;
            case TriggerType.GREATER:
                return a > b;
            case TriggerType.LESS:
                return a < b;
        }
        return false;
    }
}
