using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TutorialComet : MonoBehaviour
{
    //ここにMovePlane or 新オブジェクトとの当たり判定をとって
    //彗星を初期位置に戻す処理を書く
    private int Num = 0;//何段階まで進んでいるか
    private bool MoveFlg = false;
    public float MoveTime;//彗星が動き出す時間

    // Start is called before the first frame update
    void Start()
    {
        //時間経過するまで彗星は停止
        Observable.
        Timer(System.TimeSpan.FromMilliseconds(16.0)).
        Where(_=>!MoveFlg).
        Subscribe(_ => GetComponent<EllipseMove>().SetisActiveFalse());

        //時間経過で彗星の動作開始
        Observable.Timer(System.TimeSpan.FromSeconds(MoveTime)).Take(1).
            Subscribe(_ => MoveFlg = true);
        Observable.Timer(System.TimeSpan.FromSeconds(MoveTime)).Take(1).
            Subscribe(_ => GetComponent<EllipseMove>().ChangeActive());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool ReturnMoveFlg()
    {
        return MoveFlg;
    }
}
