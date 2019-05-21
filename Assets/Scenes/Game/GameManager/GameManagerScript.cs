using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;//Reactive Extension for Unity
using UniRx.Triggers;
using UnityEngine.SceneManagement;

[CreateAssetMenu()]
public class GameManagerScript : MonoBehaviour,IGameManager
{
    //--------------------------------------
    //ゲームプレイシーン全体を管理するクラス
    //--------------------------------------

    //ReadyStartの描画のオンオフを処理
    [Header("待機時間の描画")]
    public GameObject ReadyObject, StartObject;
    public float ReadyTime, StartTime;
    public float SceneChangeTime = 0;
    
    [Header("isActiveを操作するオブジェクト群")]
    public GameObject PlayerCore;
    public GameObject RevolutionCore;
    public GameObject Comet;
    public GameObject Comet2;

    [Header("クリアEnemy破壊数・ゲームオーバーFriend破壊数")]
    [SerializeField]
    int Friend_GameOverPoint;
    [SerializeField]
    int Enemy_GameClearPoint;

    [Header("エネミーチュートリアル用キャンバス")]
    public GameObject TutorialCanvas;

    private int FriendDestroyCount;
    private int EnemyDesroyCount;
    private bool ReadyFlg;
    // Start is called before the first frame update
    void Start()
    {
        SetParam();//条件に基づいて変数に値をセット

        //エンターが押されるまでチュートリアル用のキャンバスを表示(チュートリアル要素があれば実行)
        this.UpdateAsObservable().Where(_ => TutorialCanvas != null &&
                                            Input.GetKeyDown(KeyCode.Return)).
                                            Take(1).Subscribe(_ => ChangeReadyFlg());
        //Readyを表示
        this.UpdateAsObservable().Where(_ => ReadyFlg).Take(1).Subscribe(_ => DelayReadyChange());
        
        //シーン遷移条件を判定しシーン遷移用の関数へ
        //Whereで条件判定し、Take(1)で一回だけ実行、Subscribeで処理
        this.UpdateAsObservable().Where(_ => (EnemyDesroyCount >= Enemy_GameClearPoint)).Take(1).Subscribe(_ => ToClearScene());
        this.UpdateAsObservable().Where(_ => (FriendDestroyCount >= Friend_GameOverPoint)).Take(1).Subscribe(_ => ToGameOverScene());
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void ReadyChange()
    {
        //Ready描画を終了させてStartを描画
        ReadyObject.GetComponent<Text>().enabled = false;
        StartObject.GetComponent<Text>().enabled = true;

        Observable.Timer(System.TimeSpan.FromSeconds(StartTime)).Subscribe(_ => StartChange());//StartTime秒後にStart描画を終了
    }
    private void StartChange()
    {
        //Start描画を終了
        StartObject.GetComponent<Text>().enabled = false;
        GameObject.Find("Manager").GetComponent<Manager>().ChengeActive(true);//オプションボタン有効化
        AllChangeActive();//isActiveを切り替え
    }

    public void AllChangeActive()
    {
        PlayerCore.GetComponent<ChangeActiveInterface>().ChangeActive();
        RevolutionCore.GetComponent<ChangeActiveInterface>().ChangeActive();
        if(Comet != null) { 
        Comet.GetComponent<ChangeActiveInterface>().ChangeActive();
        }
        if (Comet2 != null)
        {
            Comet2.GetComponent<ChangeActiveInterface>().ChangeActive();
        }
    }

    public void AddFriendPoint()
    {
        FriendDestroyCount += 1;//破壊されたFriend数加算
    }

    public void AddEnemyPoint()
    {
        EnemyDesroyCount += 1;//破壊されたEnemy数加算
    }

    private void ToClearScene()
    {
        Debug.Log("クリア");
        AllChangeActive();
        //SceneChangeTimeの分だけ遅らせて
        //クリアシーンへ
        Observable.Timer(System.TimeSpan.FromSeconds(SceneChangeTime)).Subscribe(_ => GameObject.Find("Manager").GetComponent<Manager>().CallClear());
        GameObject.Find("Manager").GetComponent<Manager>().ChengeActive(false);
    }

    private void ToGameOverScene()
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
    void DelayReadyChange()
    {
        Observable.Timer(System.TimeSpan.FromSeconds(ReadyTime)).Subscribe(_ => ReadyChange());//ReadyTime秒後にStart描画に切り替える
    }
    public void ChangeReadyFlg()
    {
        //キャンバスのenabledをfalseにする処理を記入
        TutorialCanvas.SetActive(false);
        ReadyObject.GetComponent<Text>().enabled = true;//Readyの描画開始
        ReadyFlg = true;
    }
    void SetParam()
    {
        FriendDestroyCount = 0;
        EnemyDesroyCount = 0;
        StartObject.GetComponent<Text>().enabled = false;
        if (TutorialCanvas != null)//チュートリアル要素があれば
        {
            ReadyObject.GetComponent<Text>().enabled = false;
            ReadyFlg = false;
        }
        else
        {
            ReadyObject.GetComponent<Text>().enabled = true;
            ReadyFlg = true;
        }
        
    }
}