using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;//Reactive Extension for Unity

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

    [Header("isActiveを操作するオブジェクト群")]
    public GameObject PlayerCore;
    public GameObject RevolutionCore;
    public GameObject Comet;

    [Header("クリアEnemy破壊数・ゲームオーバーFriend破壊数")]
    [SerializeField]
    int Friend_GameOverPoint;
    [SerializeField]
    int Enemy_GameClearPoint;

    private int FriendDestroyCount;
    private int EnemyDesroyCount;
    // Start is called before the first frame update
    void Start()
    {
        FriendDestroyCount = 0;
        EnemyDesroyCount = 0;
        ReadyObject.GetComponent<Text>().enabled = true;
        StartObject.GetComponent<Text>().enabled = false;
        Observable.Timer(System.TimeSpan.FromSeconds(ReadyTime)).Subscribe(_ => ReadyChange());//ReadyTime秒後にStart描画に切り替える
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyDesroyCount >= Enemy_GameClearPoint)
        {
            ToClearScene();//目標数Enemy破壊したらゲームクリアシーンへ
        }

        if(FriendDestroyCount >= Friend_GameOverPoint)
        {
            ToGameOverScene();//指定数Friend破壊したらゲームオーバーシーンへ
        }
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

        AllChangeActive();//isActiveを切り替え
    }

    private void AllChangeActive()
    {
        PlayerCore.GetComponent<ChangeActiveInterface>().ChangeActive();
        RevolutionCore.GetComponent<ChangeActiveInterface>().ChangeActive();
        Comet.GetComponent<ChangeActiveInterface>().ChangeActive();
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
    }

    private void ToGameOverScene()
    {
        Debug.Log("ゲームオーバー");
    }
}