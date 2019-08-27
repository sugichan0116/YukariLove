using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using System.Linq;
using System;

[RequireComponent(typeof(UIField))]
public class BooleanByField : MonoBehaviour
{
    public enum Type
    {
        FLOAT,
        BOOL
    }

    public enum TriggerType {
        EQUAL,
        GREATER,
        LESS,
        GREATER_EQUAL,
        LESS_EQUAL,
        NOT_EQUAL
    }

    public Type type;
    public TriggerType trigger;
    public float threshold;
    public Subject<bool> onJudge = new Subject<bool>();

    // Start is called before the first frame update
    protected void Start()
    {
        GetComponent<UIField>()
            .onChanged
            .Subscribe(v =>
            {
                if(type == Type.BOOL)
                {
                    onJudge.OnNext(Convert.ToBoolean(v));
                }
                if(type == Type.FLOAT)
                {
                    onJudge.OnNext(Judge(Convert.ToSingle(v), threshold));
                }
            })
            .AddTo(this);
    }

    public bool Judge<T>(T a, T b) where T : IComparable
    {
        var r = a.CompareTo(b);

        switch (trigger)
        {
            case TriggerType.EQUAL:
                return r == 0;
            case TriggerType.GREATER:
                return r > 0;
            case TriggerType.LESS:
                return r < 0;
            case TriggerType.GREATER_EQUAL:
                return r >= 0;
            case TriggerType.LESS_EQUAL:
                return r <= 0;
            case TriggerType.NOT_EQUAL:
                return r != 0;
        }
        return false;
    }
}

public class Toaster : BooleanByField
{
    public Toast toast;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        onJudge
            .Subscribe(result =>
            {
                toast.gameObject.SetActive(result);
            })
            .AddTo(this);
    }
}