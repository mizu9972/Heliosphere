using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン遷移に必要

public class PlayerScript : MonoBehaviour
{
    public float speed = 10; //速さ
    private Rigidbody rb; // Rididbody
    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody を取得
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // カーソルキーの入力を取得
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        // カーソルキーの入力に合わせて移動方向を設定
        var movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Ridigbody に力を与えて玉を動かす
        rb.AddForce(movement * speed);
    }
    void OnCollisionEnter(Collision collision)
    {
        //以下に衝突判定を記入)
        //例
        //if (collision.gameObject.name == "Enemy")//敵と当たったら
        //{
        //    SceneManager.LoadScene("GameOver");//ゲームオーバーのsceneへ
        //}
    }
}
