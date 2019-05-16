using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    //スクリプトを設定してオブジェクトを回転させるスクリプト
    private Transform MyTrans;
    [SerializeField, Header("回転速度")]
    float RotSpeed;

    private Vector3 RotateAxis;//回転軸
    // Start is called before the first frame update
    void Start()
    {
        MyTrans = this.gameObject.GetComponent<Transform>();
        RotateAxis = new Vector3(0, 1.0f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion Rotation = Quaternion.AngleAxis(RotSpeed, RotateAxis);//回転量計算

        MyTrans.rotation = Rotation;//回転
    }
}
