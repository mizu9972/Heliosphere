﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;//Reactive Extension for Unity
using UniRx.Triggers;
using UnityEngine.SceneManagement;

public class WAVEGameManager : MonoBehaviour, IGameManager
{
    private bool FeverFlg;//フィーバー状態か
    public float SceneChangeTime = 0;

    [Header("isActiveを操作するオブジェクト群")]
    public GameObject PlayerCore;

    public GameObject Comet;

    
    [SerializeField,Header("レボリューションコア")]
    GameObject RevolutionCore1;
    [SerializeField]
    GameObject RevolutionCore2, RevolutionCore3, RevolutionCore4, RevolutionCore5;
    [Header("クリアEnemy破壊数・ゲームオーバーFriend破壊数")]
    [SerializeField]
    int Friend_GameOverPoint;
    [SerializeField]
    int Enemy_GameClearPoint;

    [SerializeField, Header("次のWAVEのGameManager")]
    GameObject nextGameManager = null;

    [SerializeField, Header("エネミーを破壊した時のスコア")]
    double EnemyBreakScore;

    [SerializeField, Header("フレンドを破壊した時のスコア")]
    double FriendBreakScore;

    [SerializeField, Header("ゲームマスター")]
    GameObject GameMaster;

    [SerializeField, Header("何WAVEとしてカウントするか")]
    int WAVECount = 1;
    private int FriendDestroyCount;

    private int EnemyDesroyCount;

    private Transform MyTrans;
    private Vector3 TargetTrans = Vector3.zero;
    private Vector3 subVector;
    [SerializeField, Header("接近してくる時間")]
    float ApproachSpeed = 1;
    private int Count = 0;//連続でエネミーを破壊した数
    private Canvas canvas;//スコア表示のキャンバス
    private Canvas nowWave;//ウェーブ
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("ScoreCanvas").GetComponent<Canvas>();
        nowWave = GameObject.Find("WaveCanvas").GetComponent<Canvas>();
        MyTrans = this.GetComponent<Transform>();
        subVector = TargetTrans - MyTrans.position;
        subVector /= ApproachSpeed;
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        //シーン遷移条件を判定しシーン遷移用の関数へ
        //Whereで条件判定し、Take(1)で一回だけ実行、Subscribeで処理
        this.UpdateAsObservable().Where(_ => (EnemyDesroyCount >= Enemy_GameClearPoint)).Take(1).Subscribe(_ => ToClearScene());
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    public void AllChangeActive()
    {
        PlayerCore.GetComponent<ChangeActiveInterface>().ChangeActive();
        if (RevolutionCore1 != null)
        {
            RevolutionCore1.GetComponent<ChangeActiveInterface>().ChangeActive();
        }
        if (RevolutionCore2 != null)
        {
            RevolutionCore2.GetComponent<ChangeActiveInterface>().ChangeActive();
        }
        if (RevolutionCore3 != null)
        {
            RevolutionCore3.GetComponent<ChangeActiveInterface>().ChangeActive();
        }
        if (RevolutionCore4 != null)
        {
            RevolutionCore4.GetComponent<ChangeActiveInterface>().ChangeActive();
        }
        if (RevolutionCore5 != null)
        {
            RevolutionCore5.GetComponent<ChangeActiveInterface>().ChangeActive();
        }
        if (Comet != null)
        {
            Comet.GetComponent<ChangeActiveInterface>().ChangeActive();
        }

    }
    public void AddFriendPoint()
    {
        FriendDestroyCount += 1;//破壊されたFriend数加算
        //Score.csのScoreCountを実行(引数は-FriendBreakScore)
        canvas.GetComponent<Score>().ScoreCount(-FriendBreakScore);
    }

    public void AddEnemyPoint()
    {
        EnemyDesroyCount += 1;//破壊されたEnemy数加算
        //Score.csのScoreCountを実行(引数はEnemyBreakScore)
        canvas.GetComponent<Score>().ScoreCount(EnemyBreakScore);
        GameMaster.GetComponent<GameMaster>().AddEnemycount();
    }
    private void ToClearScene()
    {
        //SceneChangeTimeの分だけ遅らせて
        //クリアシーンへ
        Observable.Timer(System.TimeSpan.FromSeconds(SceneChangeTime)).Subscribe(_ => nextWave());
        GameObject.Find("Manager").GetComponent<Manager>().ChengeActive(false);
    }

    private void nextWave()
    {
        if(nextGameManager != null)
        {
            //次のウェーブへ
            nextGameManager.gameObject.SetActive(true);
            nextGameManager.GetComponent<WAVEGameManager>().ApproachStart();
            GameObject.Find("GameMaster").GetComponent<GameMaster>().WAVEset(nextGameManager);
            //ここで別スクリプトのカウント関数実行
            nowWave.GetComponent<WaveCount>().WaveCounrer(WAVECount);
            this.gameObject.SetActive(false);//自身を無効化
        }else
        {
            Debug.Log("クリア");
            AllChangeActive();
            GameObject.Find("Manager").GetComponent<Manager>().CallWAVEClear();
        }
    }

    public void ToGameOverScene()
    {
        Debug.Log("ゲームオーバー");
        AllChangeActive();
        //SceneChangeTimeの分だけ遅らせて
        //ゲームオーバーシーンへ
        Observable.Timer(System.TimeSpan.FromSeconds(SceneChangeTime)).Subscribe(_ => GameObject.Find("Manager").GetComponent<Manager>().CallResult());
        GameObject.Find("Manager").GetComponent<Manager>().ChengeActive(false);
    }
    void OnSceneUnloaded(Scene scene)
    {
        Debug.Log("実行");
        GameObject gameObject;
        gameObject = GameObject.Find("Manager");
        gameObject.GetComponent<BeforScene>().ExportSceneName(scene);
    }

    public int ReturnDestroyObj()
    {
        return EnemyDesroyCount;
    }

    public void ApproachStart()
    {
        MyTrans = this.GetComponent<Transform>();
        Observable.Interval(System.TimeSpan.FromMilliseconds(16))
            .TakeWhile(_ => MyTrans.position.y < 0)
            .Subscribe(_ => ApproarchFunc());
    }
    
    void ApproarchFunc()
    {
        MyTrans.position += subVector;
    }
    
    public bool GetFeverFlg()//フィーバーフラグを受け取る
    {
        return FeverFlg;
    }

}
