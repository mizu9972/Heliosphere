using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン遷移に必要
[RequireComponent(typeof(Rigidbody))]//リジッドボディのコンポーネントをオブジェクトに追加
public class PlayerScript : MonoBehaviour
{

    public float speed = 10; //速さ
    private Rigidbody rb; // Rididbody
    private Transform MyTrans;
    //private SphereCollider MyCollider;
    public float SizeUp = 2.0f;//拡大する値

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントを取得
        rb = GetComponent<Rigidbody>();
        MyTrans = this.transform;
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
    void OnTriggerEnter(Collider other)//他のオブジェクトと衝突したら
    {
        //以下に衝突判定を記入)
        //例
        //if (collision.gameObject.name == "Enemy")//敵と当たったら
        //{
        //    SceneManager.LoadScene("GameOver");//ゲームオーバーのsceneへ
        //}
        if(other.tag=="Enemy")//衝突したオブジェクトのタグがEnemyなら
        {
            //サイズを変更
            Transform GetTrans = this.transform;//自分の位置を取得
            Vector3 SetScale = GetTrans.lossyScale;//ワールド空間サイズ情報
            Vector3 SizeUpScale = new Vector3(SetScale.x * SizeUp, SetScale.y * SizeUp, SetScale.z * SizeUp);//拡大
            MyTrans.localScale = SizeUpScale;

            Destroy(other.gameObject);//当たったオブジェクトを消滅
        }
        if(other.tag=="Comet")//彗星に衝突
        {
            //当たった時の処理
            Debug.Log("Hit"); // ログを表示する
        }
    }
}
