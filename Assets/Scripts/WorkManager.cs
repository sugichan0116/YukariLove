using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;

public class WorkManager : MonoBehaviour
{
    public Window work;
    public AudioClip sfx_click;
    public AudioClip sfx_click2;

    public void AwakeWorkWindow()
    {
        EazySoundManager.PlayUISound(sfx_click);

        var player = FindObjectOfType<Player>();
        work.gameObject.SetActive(true);
    }

    public void ApplyWork()
    {
        EazySoundManager.PlayUISound(sfx_click2);

        var player = FindObjectOfType<Player>();
        player.money += player.salary;
        player.day++;
        player.message = "お疲れ様です";
        player.IsNotWorked = false;
                
        work.gameObject.SetActive(false);
    }
}
