using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    private Vector3 MoveVector = Vector3.zero;
    private CharacterController MyController;
    private Rigidbody MyRigidB;
    private Transform MyTrans;
    private SphereCollider MyCollider;
    public float Speed = 2.0f;
    public float Gravity = 9.8f;
    public float SizeUp = 2.0f;
 
    // Start is called before the first frame update
    void Start()
    {

        MyController = GetComponent<CharacterController>();
        MyRigidB = this.GetComponent<Rigidbody>();
        MyTrans = this.transform;
        MyCollider = this.GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        var MoveHorizontal = Input.GetAxis("Horizontal");
        var MoveVertical = Input.GetAxis("Vertical");

        MoveVector = new Vector3(MoveHorizontal,  0, MoveVertical);
        MoveVector = transform.TransformDirection(MoveVector);
        MoveVector *= Speed;
        MoveVector.y -= Gravity * Time.deltaTime;

        MyController.Move(MoveVector * Time.deltaTime);
 
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            //サイズ変更
            Transform GetTrans = this.transform;
            Vector3 SetScale = GetTrans.lossyScale;
            Vector3 SizeUpScale = new Vector3(SetScale.x * SizeUp, SetScale.y * SizeUp, SetScale.z * SizeUp);
            MyTrans.localScale = SizeUpScale;

            Destroy(other.gameObject);
        }
    }
}
