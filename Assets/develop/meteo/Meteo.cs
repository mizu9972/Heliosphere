using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : MonoBehaviour,ITargetFunctionByTransform
{
    [SerializeField, Header("最初に与える加速度")]
    Vector3 InitAddForce;
    [SerializeField, Header("最初に与える力の位置")]
    Vector3 InitPowerPoint;

    [SerializeField, Header("衝突の移動量の倍率")]
    float PowerRatio;

    [SerializeField, Header("隕石の落下")]
    float Gravity_Y = -9.8f;
    private Transform MyTrans;
    private Rigidbody MyRigid;
    // Start is called before the first frame update
    void Start()
    {
        MyRigid = this.GetComponent<Rigidbody>();
        MyTrans = this.GetComponent<Transform>();
        //最初に力を与える
        MyRigid.AddForceAtPosition(InitAddForce, MyTrans.position + InitPowerPoint);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Hit(Transform Trans)
    {
        MyTrans = this.GetComponent<Transform>();
        //位置差を計算
        Vector3 subTrans = Trans.position - MyTrans.position;
        Vector3 Gravity = new Vector3(0, Gravity_Y, 0);
        //力を加える
        MyRigid.AddForceAtPosition(subTrans * PowerRatio, MyTrans.position);
        MyRigid.AddForce(Gravity);
    }

    void OnTriggerEnter(Collider other)
    {
        MyTrans = this.GetComponent<Transform>();

        var HitObjectisTarget = other.gameObject.GetComponent<ITragetFunction>();

        //当たり判定があるなら
        //ヒット
        if (HitObjectisTarget != null)
        {
            other.gameObject.GetComponent<ITragetFunction>().Hit();
        }
    }

    public void ForceSet(Vector3 inForce,Vector3 inPoint)
    {
        InitAddForce = inForce;
        InitPowerPoint = inPoint;

        MyRigid = this.GetComponent<Rigidbody>();
        MyTrans = this.GetComponent<Transform>();

        MyRigid.AddForceAtPosition(InitAddForce, MyTrans.position + InitPowerPoint);
    }
}
