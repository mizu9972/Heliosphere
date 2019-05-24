using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        ScoreCount += _Score;
    }

    public void WAVEset(GameObject SetManager)
    {
        //進行中のWAVE保存
        NowWAVEGameManager = SetManager;
    }

    public void FeverStart()
    {
        //フィーバーのキャンバスのenableをtrueに
        FeverCanvas.gameObject.SetActive(true);
        FeverCanvas.GetComponentInChildren<FeverBlink>().AlphaReset();//α値リセット
        NowWAVEGameManager.SetActive(false);

        NowFeverObj = Instantiate(FeverPrehab);
        NowFeverObj.gameObject.SetActive(true);

        MyCamera.GetComponent<SwitchPPS>().ChangeLayer("FeverPostProcessing");

        NowFeverObj.GetComponent<FeverManager>().FeverStart();

        ScoreCount = 0;
    }

    public void FeverFinish()
    {
        //フィーバーのキャンバスのenableをfalseに
        FeverCanvas.gameObject.SetActive(false);
        NowFeverObj.gameObject.SetActive(false);
        //Destroy(FeverPrehab.gameObject);

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
}
