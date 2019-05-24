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
    GameObject FeverPrehab;

    [SerializeField, Header("フィーバーモード移行スコア")]
    double FeverScore = 1;

    [SerializeField,Header("フィーバーの文字表示キャンバス")]
    public Canvas FeverCanvas;

    [SerializeField, Header("フィーバー中に生成するオブジェクト")]
    GameObject FeverObject;
    private GameObject _FeverObject;

    [SerializeField, Header("フィーバーゲージ")]
    public RawImage Fevergauge;
    private GameObject NowFeverObj;
    private int enemycount = 0;
    private double ScoreCount;//フィーバーモードへのカウント用 
    private GameObject MyCamera;
    // Start is called before the first frame update
    void Start()
    {
        this.UpdateAsObservable().Where(_ => ScoreCount >= FeverScore).Subscribe(_ => FeverStart());

        MyCamera = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CountUp(double _Score)
    {
        if(_Score >= 0)
        {
            ScoreCount += _Score;
        }

    }

    public void WAVEset(GameObject SetManager)
    {
        //進行中のWAVE保存
        NowWAVEGameManager = SetManager;
    }

    public void FeverStart()
    {
        this.UpdateAsObservable().Take(1).Subscribe(_=>Switch());//フィーバーゲージ切り替え
        //フィーバーのキャンバスのenableをtrueに
        FeverCanvas.gameObject.SetActive(true);
        FeverCanvas.GetComponentInChildren<FeverBlink>().AlphaReset();//α値リセット
        NowWAVEGameManager.SetActive(false);
        if (FeverObject != null)
        {
            _FeverObject = Instantiate(FeverObject);
        }
        NowFeverObj = Instantiate(FeverPrehab);
        NowFeverObj.gameObject.SetActive(true);

        MyCamera.GetComponent<SwitchPPS>().ChangeLayer("FeverPostProcessing");

        NowFeverObj.GetComponent<FeverManager>().FeverStart();

        ScoreCount = 0;
    }

    public void FeverFinish()
    {
        this.UpdateAsObservable().Take(1).Subscribe(_ => Switch());//フィーバーゲージ切り替え
        //フィーバーのキャンバスのenableをfalseに
        FeverCanvas.gameObject.SetActive(false);
        NowFeverObj.gameObject.SetActive(false);
        if (_FeverObject != null)
        {
            Destroy(_FeverObject);
        }
        MyCamera.GetComponent<SwitchPPS>().ChangeLayer("PostProcessing");

        NowWAVEGameManager.SetActive(true);
    }

    public void AddEnemycount()
    {
        enemycount += 1;
    }

    public void ToGameOver()
    {
        NowWAVEGameManager.GetComponent<WAVEGameManager>().ToGameOverScene();
    }
    void Switch()
    {
        //フィーバーGageのフラグの切り替え
        Fevergauge.GetComponent<FeverGauge>().SwithGauge();
    }
    public double GetFeverScore()//フィーバーに入るスコアを取得
    {
        return FeverScore;
    }
}
