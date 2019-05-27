using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameOverFunc : MonoBehaviour
{
    [SerializeField, Header("死亡演出で生成するパーティクル")]
    ParticleSystem DeathParticle;

    [SerializeField, Header("パーティクルの時間")]
    float ParticleTime;

    [SerializeField, Header("彗星が消えるまでの時間")]
    float CometDeathTime;

    [SerializeField, Header("コライダー")]
    GameObject ColliderParent;

    public void GameOver()
    {
        ColliderParent.SetActive(false);
        DeathParticle.gameObject.SetActive(true);

        //パーティクルのDurationを設定
        var ParticleMain = DeathParticle.main;
        ParticleMain.duration = ParticleTime;

        DeathParticle.Play();

        Observable.Timer(System.TimeSpan.FromSeconds(ParticleTime)).Subscribe(_ => DeathFunc());
        Observable.Timer(System.TimeSpan.FromSeconds(CometDeathTime)).Subscribe(_ => this.gameObject.SetActive(false));
    }

    void DeathFunc()
    {

        DeathParticle.Stop();
    }
}
