﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class FriendScript : MonoBehaviour,ITragetFunction
{
    private Rigidbody MyRigidB;
    public float Angle = 0.0f;//角度
    public float Speed = 2.5f;//速度
    private float AngleToRadian = Mathf.PI / 180.0f;//ラジアンに変換
    private float Radian;//ラジアンに変換後の数値保存用
    private float CenterX = 0.0f;//楕円運動の水平中心座標
    private float CenterY = 0.0f;//楕円運動の垂直中心座標
    public float RadiusX = 5.0f;//楕円運動の水平方向の半径
    public float RadiusY = 5.0f;//楕円運動の垂直方向の半径
    public Vector3 Acceleration;

    public AudioClip Explosion;//爆発音
    [SerializeField]
    AudioSource audioSource;//オーディオソース

    [SerializeField]
    GameObject GameManager;
    [SerializeField]
    GameObject ExplosionEffect;

    [SerializeField]
    float DestroyInterval;
    // Start is called before the first frame update
    void Start()
    {
        MyRigidB = this.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter()
    {
        //衝突時
    }

   public void Hit()
    {
        audioSource.PlayOneShot(Explosion);
        GameManager.GetComponent<IGameManager>().AddFriendPoint();
        GameObject explosion;
        explosion = Instantiate(ExplosionEffect, this.transform);
        this.GetComponent<MeshRenderer>().enabled = false;
        Observable.Timer(System.TimeSpan.FromSeconds(DestroyInterval)).Subscribe(_ => Destroy(this));
    }
}
