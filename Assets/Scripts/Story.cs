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

    public enum Portrait
    {
        NONE,
        STAND,
        SMILE,
        ANGRY,
        LOVE
    }

    public Speaker speaker;
    public Portrait portrait = Portrait.STAND;
    [TextArea]
    public string text;

}