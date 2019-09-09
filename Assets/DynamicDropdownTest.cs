using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UniRx;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine.SceneManagement;

public class DynamicDropdownTest : MonoBehaviour
{
    [BoxGroup("Object")]
    [SerializeField]
    GameObject obj;

    [BoxGroup("Object")]
    [SerializeField, DynamicDropdown("Paths")]
    string paths;

    public Subject<object> onChanged = new Subject<object>();

    [SerializeField, DynamicDropdown("Scenes")]
    string scene;

    [SerializeField, ReadOnly]
    string fieldValue;


    private IEnumerable Paths()
    {
        return obj?.GetComponents<MonoBehaviour>()
            .Select(c => c.GetType())
            .SelectMany(c =>
            {
                return c.GetFields()
                    .Select(f => $"{c}/{f.Name}");
            });
    }

    private IEnumerable Scenes()
    {
        //var index = SceneManager.sceneCountInBuildSettings;
        //var scenes = Enumerable.Range(0, index)
        //    .Select(i => SceneManager.GetSceneByBuildIndex(i))
        //    .Select(s => s.name);

        var scenes = EditorBuildSettings.scenes.Select(s => s.path);

        return scenes;
    }

    private void Start()
    {
        Observable
            .EveryUpdate()
            .Select(_ => GetValue(obj, paths))
            .DistinctUntilChanged()
            .Subscribe(value => {
                onChanged.OnNext(value);
            })
            .AddTo(this);

        onChanged
            .Subscribe(v =>
            {
                Debug.Log($"{v}");
                fieldValue = v.ToString();
            });
    }

    private object GetValue(GameObject obj, string path)
    {
        var p = path.Split('/');
        var c = p[0];
        var f = p[1];

        return Type.GetType(c)
            .GetField(f)
            .GetValue(obj.GetComponent(c));
    }

}

//[BoxGroup("Object")]
//[SerializeField, DynamicDropdown("Components")]
//string components;

//[BoxGroup("Object")]
//[SerializeField, DynamicDropdown("Fields")]
//string fields;

//private IEnumerable Components()
//{
//    return obj?.GetComponents<MonoBehaviour>()
//        .Select(c => c.GetType().ToString());
//}

//private IEnumerable Fields()
//{
//    return Type.GetType(components)?.GetFields()
//        .Select(c => c.Name);
//}