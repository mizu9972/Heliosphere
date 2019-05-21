using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveByGravity : MonoBehaviour, IAffectbyBH
{
    [SerializeField, Header("影響の強さの倍率")]
    float GravityForceRatio;


    private Transform MyTrans;
    private Rigidbody MyRigid;

    // Start is called before the first frame update
    void Start()
    {
        MyRigid = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GravityEffect(Transform BHTrans, Rigidbody BHRigid)
    {
        MyTrans = this.GetComponent<Transform>();
        //ブラックホールとの位置差
        Vector3 subVector = new Vector3(
        MyTrans.position.x - BHTrans.position.x,
        MyTrans.position.y - BHTrans.position.y,
        MyTrans.position.z - BHTrans.position.z
    );

        Vector3 gravity = -6 * subVector.normalized * (BHRigid.mass * MyRigid.mass) / (subVector.sqrMagnitude);
        gravity *= GravityForceRatio;

        MyRigid.AddForceAtPosition(gravity, MyTrans.position);
    }
}
