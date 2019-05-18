using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TutorialComet : MonoBehaviour
{
    //ここにMovePlane or 新オブジェクトとの当たり判定をとって
    //彗星を初期位置に戻す処理を書く
    private int StepNum = 0;//何段階まで進んでいるか
    private bool MoveFlg = false;
    public float MoveTime;//彗星が動き出す時間
    private int DestroyNum = 0;//エネミーを破壊した数
    private Transform MyTrans;
    public GameObject GameManager;
    public GameObject PlaneNo1;//一番目の壁
    public GameObject PlaneNo2;//二番目の壁(左)
    public GameObject PlaneNo3;//二番目の壁(右)
    // Start is called before the first frame update
    void Start()
    {
        MyTrans = this.GetComponent<Transform>();
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
        if (GameManager != null)
        {
            DestroyNum = GameManager.GetComponent<GameManagerScript>().ReturnDestroyObj();
        }
        OpenWall();//壁の開放
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(StepNum == 0)//何も破壊してなければPlaneNo1と当たった場合のみ位置をリセット
        {
            if(other.name== "MovePlaneNo1")
            {
                //彗星の位置をリセット
                GetComponent<EllipseMove>().SetStartPosition();
            }
            
        }
        else if(StepNum==1)
        {
            if (other.name == "MovePlaneNo2-1"||other.name== "MovePlaneNo2-2")
            {
                //彗星の位置をリセット
                GetComponent<EllipseMove>().SetStartPosition();
            }
        }
        
    }
    public bool ReturnMoveFlg()
    {
        return MoveFlg;
    }
    void OpenWall()
    {
        if (DestroyNum == 1)//1番目の壁を開放
        {
            PlaneNo1.GetComponent<MovePlane>().StartFade();
            StepNum = 1;
        }
        if (DestroyNum == 2)//2番目の壁を開放
        {
            PlaneNo2.GetComponent<MovePlane>().StartFade();
            PlaneNo3.GetComponent<MovePlane>().StartFade();
        }
    }
}
