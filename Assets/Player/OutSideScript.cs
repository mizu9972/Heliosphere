using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutSideScript : MonoBehaviour
{
    private Transform MyTrans;
    private SphereCollider MyCollider;
    public float SizeUp = 2.0f;//拡大する値

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントを取得
        MyTrans = this.transform;
        MyCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)//他のオブジェクトと衝突したら
    {

        //以下に衝突判定を記入)
        //例
        //if (collision.gameObject.name == "Enemy")//敵と当たったら
        //{
        //    SceneManager.LoadScene("GameOver");//ゲームオーバーのsceneへ
        //}
        if (other.tag == "Enemy")//衝突したオブジェクトのタグがEnemyなら
        {
            Debug.Log("hit");
            //サイズを変更
            Transform GetTrans = this.transform;//自分の位置を取得
            Vector3 SetScale = GetTrans.lossyScale;//ワールド空間サイズ情報
            Vector3 SizeUpScale = new Vector3(SetScale.x + SizeUp, SetScale.y + SizeUp, SetScale.z + SizeUp);//拡大
            MyTrans.localScale = SizeUpScale;

            Destroy(other.gameObject);//当たったオブジェクトを消滅
        }

        if (other.tag == "Comet")//彗星に衝突
        {
            //当たった時の処理
            Debug.Log("Hit"); // ログを表示する
        }
    }
}
