using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    private Transform MyTrans;
    private Vector3 Wark_Size;//サイズの避難用
    private Vector3 Wark_Pos;//座標の避難用
    public Transform Parent;//親オブジェクトの位置情報
    public Transform SunTrans;


    [SerializeField]
    private GameObject Target = null;

    [SerializeField]
    float SizeUp = 1;//拡大するやつ

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントの取得
        MyTrans = this.GetComponent<Transform>();
        Wark_Size = this.GetComponent<Transform>().localScale;

    }

    // Update is called once per frame
    void Update()
    {
        Wark_Pos = Parent.position;//位置を取得
        Vector3 MyPos = MyTrans.position;//自分の座標
        Vector3 SunPos = SunTrans.position;//太陽の座標

        //太陽との座標値の差を計算
        float dx = SunPos.x - MyPos.x;//X座標
        float dz = SunPos.z - MyPos.z;//Z座標

        //太陽との距離
        float Distance = Mathf.Sqrt(dz * dz + dx * dx);

        ColliderAngleSet(dz, dx);//角度を設定
        //オブジェクトの拡大
        SetColliderSizeUp(Distance);
    }
    void ColliderAngleSet(float dz, float dx)
    {
        //伸びる方向の角度を計算して設定
        this.transform.LookAt(Target.transform);//ターゲットの方を向く

        //向いている方向に対して-1を乗算(ターゲットに対して背を向ける)
        this.transform.forward *= -1;
    }

    void SetColliderSizeUp(float Distance)
    {
        //距離によって当たり判定を伸ばす
        MyTrans.localScale = Wark_Size;//一度拡大縮小をリセット
        MyTrans.position = Wark_Pos;

        float DistanceRatio = SizeUp / Distance;//割合

        //オブジェクトの拡大処理
        Transform GetTrans = this.transform;//自分の位置を取得
        Vector3 WarkPos = MyTrans.position;//Z座標修正用
        Vector3 SetScale = GetTrans.lossyScale;//ワールド空間サイズ情報
        Vector3 SizeUpScale = new Vector3(SetScale.x,
                                        SetScale.y,
                                        SetScale.z + DistanceRatio);//拡大
        MyTrans.localScale = SizeUpScale;

        //Z座標の修正
        MyTrans.position += this.transform.forward * DistanceRatio;
    }
}
