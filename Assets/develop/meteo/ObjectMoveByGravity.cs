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

    public void GravityEffect(Transform BHTrans)
    {
        MyTrans = this.GetComponent<Transform>();

        //ブラックホールとの位置差
        Vector3 subVector = new Vector3(
            BHTrans.position.x - MyTrans.position.x,
            BHTrans.position.y - MyTrans.position.y,
            BHTrans.position.z - MyTrans.position.z
            );

        subVector = transform.TransformDirection(subVector);
        subVector *= GravityForceRatio;

        MyRigid.AddForceAtPosition(subVector, MyTrans.position);
    }
}
