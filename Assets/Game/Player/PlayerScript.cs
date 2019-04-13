using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン遷移に必要
//[RequireComponent(typeof(Rigidbody))]//リジッドボディのコンポーネントをオブジェクトに追加

public class PlayerScript : MonoBehaviour, ChangeActiveInterface
{

    public float speed = 3.0f; //速さ
    
    public float Gravity = 9.8f;

    protected Vector3 MoveVector = Vector3.zero;
    
    protected CharacterController controller;

    public bool isActive = false;

    private float MoveHorizontal;
    private float MoveVertical;
    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントを取得
        controller = GetComponent<CharacterController>();
        
    }
    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            //isActiveがtrueなら操作受付
            Controll();
        }

        MoveVector = new Vector3(MoveHorizontal, 0, MoveVertical);
        MoveVector = transform.TransformDirection(MoveVector);
        MoveVector *= speed;
        MoveVector.y -= Gravity * Time.deltaTime;

        controller.Move(MoveVector * Time.deltaTime);
    }

    private void Controll()
    {
        //キー操作
        MoveHorizontal = Input.GetAxis("Horizontal");
        MoveVertical = Input.GetAxis("Vertical");
    }

    public void ChangeActive()
    {
        isActive = !isActive;
    }
}
