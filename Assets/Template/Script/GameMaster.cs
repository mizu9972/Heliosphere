using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class GameMaster : MonoBehaviour
{
    [SerializeField,Header("最初のWAVE設定")]
    GameObject NowWAVEGameManager;

    [SerializeField, Header("フィーバーWAVEプリセット")]
    GameObject[] FeverPrehab = new GameObject[15];
    
    [SerializeField, Header("フィーバーモード移行スコア")]
    double FeverScore = 1;

    [SerializeField,Header("フィーバーの文字表示キャンバス")]
    public Canvas FeverCanvas;

    [SerializeField, Header("フィーバー中に生成するオブジェクト")]
    GameObject FeverObject;
    private GameObject _FeverObject;

    [Header("フィーバーゲージペアレント")]
    public GameObject FeverGaugeParent;

    [SerializeField, Header("フィーバーゲージ")]
    public RawImage Fevergauge;
    [Header("HPゲージ")]
    public GameObject HPGageCanvas;

    [SerializeField, Header("フィーバー突入で一瞬表示するキャンバス")]
    GameObject FeverPopCanvas;

    [SerializeField]
    float FeverPopDelayTime = 0,FeverPopTime = 0.5f;

    private GameObject NowFeverObj;
    private int enemycount = 0;
    private double ScoreCount;//フィーバーモードへのカウント用 
    private GameObject MyCamera;
    private GameObject Manager;
    private int HP;
    // Start is called before the first frame update
    void Start()
    {
        // マウスカーソルを削除する
        UnityEngine.Cursor.visible = false;

        HPGageCanvas = GameObject.Find("HPGageParent");
        Manager = GameObject.Find("Manager");
        this.UpdateAsObservable().Where(_ => ScoreCount >= FeverScore).Subscribe(_ => FeverStart());

        MyCamera = GameObject.FindWithTag("MainCamera");
        HP = HPGageCanvas.GetComponent<HPGageParent>().GetNowHP();//HPの取得
        this.UpdateAsObservable().Where(_ => HP <= 0).Take(1).Subscribe(_ => ToGameOver());
    }

    // Update is called once per frame
    void Update()
    {
        HP = HPGageCanvas.GetComponent<HPGageParent>().GetNowHP();//HPの取得
    }

    public void CountUp(double _Score)
    {


        ScoreCount += _Score;


    }

    public void CountDown(float _Ratio)
    {
        ScoreCount -= _Ratio * FeverScore;

        if(ScoreCount <= 0)
        {
            ScoreCount = 0;
        }
    }

    public void WAVEset(GameObject SetManager)
    {
        //進行中のWAVE保存
        NowWAVEGameManager = SetManager;
    }

    public void FeverStart()
    {
        var FeedInPanel = GetComponentInChildren<FeedIn>();
        int RandomNum = (int)Random.Range(0.0f, 14.0f);
        //フェードイン
        FeedInPanel.Init(255, 0, 0.5f);

        //フィーバーGageのフラグの切り替え
        Fevergauge.GetComponent<FeverGauge>().SwithGauge(true);
        //フィーバーのキャンバスのenableをtrueに
        FeverCanvas.gameObject.SetActive(true);
        FeverCanvas.GetComponentInChildren<FeverBlink>().AlphaReset();//α値リセット
        //WAVE1セット
        FeverGaugeParent.GetComponent<GaugeParent>().setWAVE1();
        //通常ステージ退避
        NowWAVEGameManager.SetActive(false);

        //HPゲージ非表示
        HPGageCanvas.SetActive(false);

        //パーティクル生成
        if (FeverObject != null)
        {
            _FeverObject = Instantiate(FeverObject);
        }
        //フィーバールーチン生成
        NowFeverObj = Instantiate(FeverPrehab[RandomNum]);
        NowFeverObj.gameObject.SetActive(true);
        //見た目変更
        MyCamera.GetComponent<SwitchPPS>().ChangeLayer("FeverPostProcessing");

        //フィーバー起動
        NowFeverObj.GetComponent<FeverManager>().FeverStart();

        //フィーバー突入演出
        Observable.Timer(System.TimeSpan.FromSeconds(FeverPopDelayTime)).Subscribe(_ => FeverPopFunc());
        ScoreCount = 0;

    }

    public void FeverFinish()
    {
        var FeedInPanel = GetComponentInChildren<FeedIn>();
        //フェードイン
        FeedInPanel.Init(255, 0, 0.5f);
        //フィーバーGageのフラグの切り替え
        Fevergauge.GetComponent<FeverGauge>().SwithGauge(false);
        //フィーバーのキャンバスのenableをfalseに
        FeverCanvas.gameObject.SetActive(false);
        NowFeverObj.gameObject.SetActive(false);
        if (_FeverObject != null)
        {
            Destroy(_FeverObject);
        }
        MyCamera.GetComponent<SwitchPPS>().ChangeLayer("PostProcessing");

        //HPゲージ表示
        HPGageCanvas.SetActive(true);
        NowWAVEGameManager.SetActive(true);
        NowWAVEGameManager.GetComponent<WAVEGameManager>().ApproachStart();
    }

    public void AddEnemycount()
    {
        enemycount += 1;
    }

    public void ToGameOver()
    {
        // マウスカーソルを表示する
        UnityEngine.Cursor.visible = true;
        //ゲームオーバーのBGM再生
        Manager.GetComponent<AudioManager>().PlayResult(AudioManager.AudioType.GameOver);
        NowWAVEGameManager.GetComponent<WAVEGameManager>().ToGameOverScene();
    }
    //void Switch()//Gageのフラグ切り替え
    //{
        //フィーバーGageのフラグの切り替え
        //Fevergauge.GetComponent<FeverGauge>().SwithGauge();
    //}
    public double GetFeverScore()//フィーバーに入るスコアを取得
    {
        return FeverScore;
    }
    public double GetNowScore()
    {
        return ScoreCount;
    }
    public void FrendDestroyCount()
    {
        //HPGageParentの関数実行
        if(HPGageCanvas!=null)
        {
            HPGageCanvas.GetComponent<HPGageParent>().DestroyFriendCount();
        }
    }

    void FeverPopFunc()
    {
        FeverPopCanvas.SetActive(true);
        Observable.Timer(System.TimeSpan.FromSeconds(FeverPopTime))
            .Where(_ => FeverPopCanvas != null)
            .Subscribe(_ => FeverPopCanvas.SetActive(false));
    }
}
