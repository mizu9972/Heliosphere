using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Particle : MonoBehaviour
{

    private Transform MyTrans;
    public Transform sunTrans;

    public float LifeTimeRatiobyDistance;
    public float MaxLifeTime;
    public bool isActiveLifeTimeSet;
    private Quaternion PreRotate;
    private ParticleSystem MyParticleState;

    // Start is called before the first frame update
    void Start()
    {
        MyParticleState = this.GetComponent<ParticleSystem>();//ParticleSystemComponent取得
        MyTrans = this.GetComponent<Transform>();

        PreRotate = Quaternion.Euler(0, 90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 MyPos = MyTrans.position;//自分の座標
        Vector3 sunPos = sunTrans.position;//太陽の座標

        //彗星と太陽の座標値の差を計算
        float dy = sunPos.z - MyPos.z;
        float dx = sunPos.x - MyPos.x;

        //彗星と太陽の距離
        float Distance = Mathf.Sqrt(dy * dy + dx * dx);

        if (isActiveLifeTimeSet)
        {
            ParticleLifetimeSet(Distance);//生存時間を設定
        }
    }

    void ParticleLifetimeSet(float Distance)
    {
        //距離によって生存時間を変更する = 尾の長さを変更する
        float DistanceRatio = 1 / Distance;//割合

        var ParticleMain = MyParticleState.main;

        float SetLifeTime = LifeTimeRatiobyDistance * DistanceRatio;
        if (SetLifeTime > MaxLifeTime)
        {
            SetLifeTime = MaxLifeTime;
        }
        ParticleMain.startLifetime = SetLifeTime;


    }
}
