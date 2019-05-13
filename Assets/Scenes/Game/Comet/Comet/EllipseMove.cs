using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EllipseMove : MonoBehaviour, ChangeActiveInterface
{

    [SerializeField,Header("角度")]
    private float Angle = 0.0f;//角度
    [SerializeField, Header("速度")]
    private float Speed = 2.5f;//速度
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
    // Start is called before the first frame update
    void Start()
    {
        MyRigidB = this.GetComponent<Rigidbody>();
        setPosition();//初期位置へ
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isActive)
        {
            setPosition();//移動
        }
    }

    private void setPosition()
    {
        //角度を更新して彗星を移動させる関数
        Angle += Speed;
        Angle = (Angle % 360 + 360) % 360;//角度を0から360の間の数値に調整
        Radian = Angle * AngleToRadian;//ラジアンに変換

        MyRigidB.AddForce(Acceleration, ForceMode.Acceleration);//加速度(inspectorで調整)
        transform.position = new Vector3(CenterPos.x + Mathf.Cos(Radian) * Radius.x, 0, CenterPos.y + Mathf.Sin(Radian) * Radius.y);
    }

    public void ChangeActive()
    {
        isActive = !isActive;
    }
}
