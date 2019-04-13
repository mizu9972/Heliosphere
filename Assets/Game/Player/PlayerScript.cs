using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン遷移に必要
//[RequireComponent(typeof(Rigidbody))]//リジッドボディのコンポーネントをオブジェクトに追加
public class PlayerScript : MonoBehaviour
{

    public float speed = 3.0f; //速さ
    
    public float Gravity = 9.8f;

    /*private Rigidbody rb;*/ // Rididbody

    private Vector3 MoveVector = Vector3.zero;
    
    CharacterController controller;

    

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントを取得
        controller = GetComponent<CharacterController>();
        //rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        var MoveHorizontal = Input.GetAxis("Horizontal");
        var MoveVertical = Input.GetAxis("Vertical");

        MoveVector = new Vector3(MoveHorizontal, 0, MoveVertical);
        MoveVector = transform.TransformDirection(MoveVector);
        MoveVector *= speed;
        MoveVector.y -= Gravity * Time.deltaTime;

        controller.Move(MoveVector * Time.deltaTime);
    }
   
}
