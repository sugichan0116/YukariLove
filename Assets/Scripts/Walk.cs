using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using UniRx.Triggers;
using DG.Tweening;
using System;

public class Walk : MonoBehaviour
{
    public GameObject room;
    public float width;
    public float velocity;

    // Start is called before the first frame update
    void Start()
    {
        var animator = GetComponent<Animator>();
        var sprite = GetComponent<SpriteRenderer>();
        
        Observable
            .EveryUpdate()
            .Subscribe(_ => {
                animator.SetFloat("Speed", Mathf.Abs(velocity));
                sprite.flipX = velocity < 0;

                var delta = new Vector3(velocity * Time.deltaTime, 0f);
                room.transform.position += delta;
            })
            .AddTo(gameObject);

        Observable
            .Interval(TimeSpan.FromSeconds(4))
            .Subscribe(_ =>
            {
                velocity = (int)UnityEngine.Random.Range(-2f, 2f);
            })
            .AddTo(gameObject);
        
        Observable
            .EveryUpdate()
            .Where(_ => width <= Mathf.Abs(room.transform.position.x))
            .Where(_ => 0 < room.transform.position.x * velocity)
            .Subscribe(_ => {
                velocity *= -1;
            })
            .AddTo(gameObject);
    }
}
