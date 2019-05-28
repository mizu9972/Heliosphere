using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

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

    [SerializeField, Header("彗星コア")]
    GameObject CometCore;

    Vector3 CometSize;
    [SerializeField, Header("CometTrail")]
    ParticleSystem CometParticle;

    Vector3 CometParticleSize;

    private GameObject EffectBox;
    void Start()
    {
        EffectBox = GameObject.Find("EffectBox");
    }
    void Update()
    {

    }

    public void GameOver()
    {
        CometSize = CometCore.transform.localScale;
        CometParticleSize = CometParticle.transform.localScale;
        CometDeathTime *= 1000;

        ColliderParent.SetActive(false);

        var _DeathParticle = Instantiate(DeathParticle, this.gameObject.transform); 
        //パーティクルのDurationを設定
        var ParticleMain = _DeathParticle.main;
        ParticleMain.duration = ParticleTime;

        DeathParticle.gameObject.SetActive(true);
        if (EffectBox != null)
        {
            _DeathParticle.transform.parent = EffectBox.transform;
        }
        _DeathParticle.Play();
        
        this.UpdateAsObservable().Subscribe(_ => CometFunc());

        Observable.Timer(System.TimeSpan.FromSeconds(ParticleTime)).Subscribe(_ => DeathFunc());
        Observable.Timer(System.TimeSpan.FromMilliseconds(CometDeathTime)).Subscribe(_ => this.gameObject.SetActive(false));
    }

    void CometFunc()
    {
        if(CometCore == null || CometParticle == null)
        {
            return;
        }

        CometCore.transform.localScale -= new Vector3(CometSize.x / (CometDeathTime / 16), CometSize.y / (CometDeathTime / 16), CometSize.z / (CometDeathTime / 16));
        CometParticle.transform.localScale -= new Vector3(CometParticleSize.x / (CometDeathTime / 16), CometParticleSize.y / (CometDeathTime / 16), CometParticleSize.z / (CometDeathTime / 16));

    }
    void DeathFunc()
    {

        DeathParticle.Stop();
    }
}
