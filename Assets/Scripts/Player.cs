using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public int money;
    public int salary;
    public int love;
    public int needLove;
    public int loveProgress;
    public int level;
    public int day;
    public string message;
    public bool IsNotWorked;

    private void Start()
    {
        var statistic = GetComponent<CardStatistic>();
        var work = FindObjectOfType<WorkManager>();

        Observable
            .EveryUpdate()
            .Subscribe(fillVolume => {
                loveProgress = (love + statistic.SumLove()) * 100 / needLove;
            })
            .AddTo(gameObject);
        
        Observable
            .EveryUpdate()
            .Where(_ => love >= needLove)
            .Subscribe(_ => {
                message = "LoveUP!\nマスター好き！";
                love -= needLove;
                needLove *= 3;
                salary += 100 * level;
                level++;
            })
            .AddTo(gameObject);

        var replacer = FindObjectOfType<Replacer>();
        this
            .ObserveEveryValueChanged(p => p.IsNotWorked)
            .Subscribe(notwork => {
                replacer.gameObject.SetActive(!notwork);
            });

        Observable
            .EveryUpdate()
            .Subscribe(_ => {
                work.gameObject.SetActive(IsNotWorked);
            })
            .AddTo(gameObject);
    }
}
