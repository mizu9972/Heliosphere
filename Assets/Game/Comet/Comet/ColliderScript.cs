using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    private Transform MyTrans;
    public Transform SunTrans;

    private float SizeUp = 1;//拡大するやつ
    [SerializeField]
    private GameObject Target = null;

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントの取得
        MyTrans = this.GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 MyPos = MyTrans.position;//自分の座標
        Vector3 SunPos = SunTrans.position;//太陽の座標

        //太陽との座標値の差を計算
        float dx = SunPos.x - MyPos.x;//X座標
        float dz = SunPos.z - MyPos.z;//Z座標

        //太陽との距離
        float Distance = Mathf.Sqrt(dz * dz + dx * dx);

        ColliderAngleSet(dz, dx);//角度を設定
        //オブジェクトの拡大
    }
    void ColliderAngleSet(float dz,float dx)
    {
        //伸びる方向の角度を計算して設定
        this.transform.LookAt(Target.transform);//ターゲットの方を向く

        //向いている方向に対して-1を乗算(ターゲットに対して背を向ける)
        this.transform.forward *= -1;
    }

    void SetColliderSizeUp(float Distance)
    {
        //距離によって当たり判定を伸ばす
        float DistanceRatio = 1 / Distance;//割合

        //オブジェクトの拡大処理

    }
}
