using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン遷移に必要
//[RequireComponent(typeof(Rigidbody))]//リジッドボディのコンポーネントをオブジェクトに追加

public class PlayerScript : MonoBehaviour, ChangeActiveInterface, IMoveOperate
{

    public float speed = 3.0f; //速さ
    
    public float Gravity = 9.8f;//重力

   

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


    }

    public void MoveControll()
    {
        if (isActive)
        {
            //キー操作
            MoveHorizontal = Input.GetAxis("Horizontal");
            MoveVertical = Input.GetAxis("Vertical");
        }else
        {
            //キー入力を受け付けていないときは移動成分を0に
            MoveHorizontal = 0;
            MoveVertical = 0;
        }

        //移動反映
        MoveVector = new Vector3(MoveHorizontal, 0, MoveVertical);
        MoveVector = transform.TransformDirection(MoveVector);
        MoveVector *= speed;//速度補正

        MoveVector.y -= Gravity * Time.deltaTime;//重力反映

        controller.Move(MoveVector * Time.deltaTime);//移動
    }

    public void ChangeActive()
    {
        isActive = !isActive;//切り替え
    }
    
}
