using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(
  fileName = "Story",
  menuName = "ScriptableObject/Story",
  order = 0)
]
[System.Serializable]
public class Story : ScriptableObject
{
    public List<Speaking> messages;
}

[System.Serializable]
public class Speaking
{
    public enum Speaker
    {
        MASTER,
        YUKARI
    }

    public Speaker speaker;
    [TextArea]
    public string text;

}