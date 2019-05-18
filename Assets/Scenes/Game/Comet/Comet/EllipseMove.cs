using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[RequireComponent(typeof(Rigidbody))]
public class EllipseMove : MonoBehaviour, ChangeActiveInterface
{
    [SerializeField,Header("太陽のコア")]
    private Transform sunTrans;

    private Transform MyTrans;//自身
    [SerializeField,Header("角度")]
    private float Angle = 0.0f;//角度
    [SerializeField, Header("速度")]
    private float Speed = 1.5f;//速度
    private float AngleToRadian = Mathf.PI / 180.0f;//ラジアンに変換
    private float Radian;//ラジアンに変換後の数値保存用
    private Rigidbody MyRigidB;

    [Header("楕円運動の中心座標")]
    [SerializeField]
    private Vector2 CenterPos;

    [Header("楕円運動の半径")]
    [SerializeField]
    private Vector2 Radius;

    [Header("加速度")]
    [SerializeField]
    private Vector3 Acceleration;
    
    private bool isActive;
    private Transform StartPosition;//開始位置
    // Start is called before the first frame update
    void Start()
    {
        MyRigidB = this.GetComponent<Rigidbody>();
        MyTrans = this.GetComponent<Transform>();
        setPosition();//初期位置へ
        
        GetStartPosition();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 MyPos = MyTrans.position;//自分の座標
        Vector3 sunPos = sunTrans.position;//太陽の座標

        //彗星と太陽の座標値の差を計算
        float dy = sunPos.z - MyPos.z;
        float dx = sunPos.x - MyPos.x;

        ParticleAngleSet(dy, dx);
        if (isActive)
        {
            setPosition();//移動
        }
        //初期位置をセットする（これを関数で渡す）
    }

    private void setPosition()
    {
        //角度を更新して彗星を移動させる関数
        Angle += Speed;
        Angle = (Angle % 360 + 360) % 360;//角度を0から360の間の数値に調整
        Radian = Angle * AngleToRadian;//ラジアンに変換

        MyRigidB.AddForce(Acceleration, ForceMode.Acceleration);//加速度(inspectorで調整)
        this.transform.position = new Vector3(CenterPos.x + Mathf.Cos(Radian) * Radius.x, 0, CenterPos.y + Mathf.Sin(Radian) * Radius.y);
    }

    public void ChangeActive()
    {
        isActive = !isActive;
    }
    void ParticleAngleSet(float dy, float dx)
    {       
        //パーティクル発射の角度を計算して設定
        Vector3 axis = new Vector3(0, 1.0f, 0);//回転の軸を指定
        float AngleinRadian;//太陽と彗星の角度

        //角度を計算
        AngleinRadian = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        Quaternion Rotation = Quaternion.AngleAxis(180 - AngleinRadian, axis);//回転量を設定

        MyTrans.rotation = Rotation;//反映

    }
    public void SetisActiveFalse()
    {
        isActive = false;
        Observable.Timer(System.TimeSpan.FromMilliseconds(16.0)).
            Where(_=>!GetComponent<TutorialComet>().ReturnMoveFlg()).Subscribe(_ => SetisActiveFalse());
    }
    public void GetStartPosition()//開始位置を送信
    {

        StartPosition = this.transform;//初期位置をセット
        
    }
    public void SetStartPosition()
    {
        Angle = 0.0f;
        Speed = 1.5f;
        MyTrans = StartPosition;
    }
}
