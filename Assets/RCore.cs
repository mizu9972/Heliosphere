using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----------------------------------------------------------
//ターゲットを回転させる中心となる親オブジェクトのスクリプト
//----------------------------------------------------------
public class RCore : MonoBehaviour
{
    private float Angle = 0;//角度(度数法)
    private float Radian;//ラジアン角
    public float RotateSpeed = 0;//回転速度(inspectorで調整)

    private float AngleToRadian = Mathf.PI / 180.0f;//ラジアンに変換

    private Transform MyTrans;
    // Start is called before the first frame update
    void Start()
    {
        MyTrans = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Angle += RotateSpeed;//回転
        Angle = Angle % 360;//360度を超えないように

        Radian = Angle * AngleToRadian;

        MyTrans.rotation = Quaternion.Euler(0, Angle, 0);
    }
}
