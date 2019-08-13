using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkManager : MonoBehaviour
{
    public Window work;

    public void AwakeWorkWindow()
    {
        var player = FindObjectOfType<Player>();
        work.gameObject.SetActive(true);
    }

    public void ApplyWork()
    {
        var player = FindObjectOfType<Player>();
        player.money += player.salary;
        player.day++;
        player.message = "お疲れ様です";
        player.IsNotWorked = false;
                
        work.gameObject.SetActive(false);
    }
}
