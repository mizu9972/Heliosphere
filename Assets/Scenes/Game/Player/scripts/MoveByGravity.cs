using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByGravity : MonoBehaviour, IAffectbyBH
{
    [SerializeField, Header("影響の強さの倍率")]
    float GravityForceRatio;

    private Transform MyTrans;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void GravityEffect(Transform BHTrans,Rigidbody BHRigid)
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
        controller.Move(subVector * Time.deltaTime);
    }
}