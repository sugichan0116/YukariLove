using UnityEngine;
using UniRx;
using System.Linq;
using System;
using NaughtyAttributes;
using UnityEditor;

public class UIField : MonoBehaviour
{
    public GameObject target;
    [HideInInspector]
    public string component;
    [HideInInspector]
    public string fieldName;
    
    [HideInInspector]
    public int componentIndex;
    [HideInInspector]
    public int fieldIndex;

    public Subject<object> onChanged = new Subject<object>();

    // Start is called before the first frame update
    void Start()
    {
        Observable
            .EveryUpdate()
            .Select(_ => GetValue())
            .DistinctUntilChanged()
            .Subscribe(value => {
                onChanged.OnNext(value);
            })
            .AddTo(gameObject);

        onChanged.OnNext(GetValue());
    }

    private object GetValue()
    {
        var field = Type.GetType(component).GetField(fieldName);
        var value = field.GetValue(target.GetComponent(component));

        return value;
    }
}

// ここからエディター上でのみ有効
#if UNITY_EDITOR

// エディター拡張クラス
[CustomEditor(typeof(UIField))]
public class ExtendedEditor : Editor
{// Editor クラスを継承
    // Extend クラスの変数を扱うために宣言
    UIField extend;

    void OnEnable()
    {// 最初に実行
        // Extend クラスに target を代入
        extend = (UIField)target;
    }

    public override void OnInspectorGUI()
    {// Inspector に表示
        base.OnInspectorGUI();

        if (extend.target == null) return;

        var components = extend.target.GetComponents<MonoBehaviour>().Select(c => c.GetType().ToString()).ToArray();
        extend.componentIndex = DynamicPopupGUI("Component", extend.componentIndex, components);
        extend.component = components[extend.componentIndex];

        if(Type.GetType(extend.component) != null)
        {
            var fields = Type.GetType(extend.component).GetFields().Select(c => c.Name).ToArray();
            extend.fieldIndex = DynamicPopupGUI("Field", extend.fieldIndex, fields);
            extend.fieldName = fields[extend.fieldIndex];
        }
        

    }

    private int DynamicPopupGUI(string text, int selectedIndex, string[] displayOptions)
    {
        // ツールチップ入りラベルの作成
        var label = new GUIContent(text);
        
        // プルダウンメニューの作成
        return displayOptions.Length > 0 ? EditorGUILayout.Popup(label, selectedIndex, displayOptions) : 0;
    }
}
// ここまでエディター上でのみ有効
#endif