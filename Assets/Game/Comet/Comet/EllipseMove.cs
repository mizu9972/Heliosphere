using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EllipseMove : MonoBehaviour, ChangeActiveInterface
{
    public float Angle = 0.0f;//角度
    public float Speed = 2.5f;//速度
    private float AngleToRadian = Mathf.PI / 180.0f;//ラジアンに変換
    private float Radian;//ラジアンに変換後の数値保存用
    private Rigidbody MyRigidB;
    public float CenterX = 0.0f;//楕円運動の水平中心座標
    public float CenterY = 0.0f;//楕円運動の垂直中心座標
    public float RadiusX = 10.0f;//楕円運動の水平方向の半径
    public float RadiusY = 5.0f;//楕円運動の垂直方向の半径
    public Vector3 Acceleration;
    public bool isActive;
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
        transform.position = new Vector3(CenterX + Mathf.Cos(Radian) * RadiusX, 0, CenterY + Mathf.Sin(Radian) * RadiusY);
    }

    public void ChangeActive()
    {
        isActive = !isActive;
    }
}
