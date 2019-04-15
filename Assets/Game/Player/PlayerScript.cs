﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン遷移に必要
//[RequireComponent(typeof(Rigidbody))]//リジッドボディのコンポーネントをオブジェクトに追加

public class PlayerScript : MonoBehaviour, ChangeActiveInterface
{

    public float speed = 3.0f; //速さ
    
    public float Gravity = 9.8f;

    //移動範囲制限用の変数
    public float MinX = -5.0f;
    public float MaxX = 5.0f;
    public float MinZ = -5.0f;
    public float MaxZ = 5.0f;

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

        //移動範囲の制限
        this.transform.position = (new Vector3(Mathf.Clamp(this.transform.position.x, MinX, MaxX),
                                               this.transform.position.y,
                                               Mathf.Clamp(this.transform.position.z, MinZ, MaxZ)));

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
