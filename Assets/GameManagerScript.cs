using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;//Reactive Extension for Unity

[CreateAssetMenu()]
public class GameManagerScript : MonoBehaviour
{
    //--------------------------------------
    //ゲームプレイシーン全体を管理するクラス
    //--------------------------------------

    //ReadyStartの描画のオンオフを処理
    public GameObject ReadyObject, StartObject;
    public float ReadyTime, StartTime;

    [Header("isActiveを操作するオブジェクト群")]
    public GameObject PlayerCore;
    public GameObject RevolutionCore;
    public GameObject Comet;

    // Start is called before the first frame update
    void Start()
    {
        ReadyObject.GetComponent<Text>().enabled = true;
        StartObject.GetComponent<Text>().enabled = false;
        Observable.Timer(System.TimeSpan.FromSeconds(ReadyTime)).Subscribe(_ => ReadyChange());//ReadyTime秒後にStart描画に切り替える
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

        AllChangeActive();//isActiveを切り替え
    }

    private void AllChangeActive()
    {
        PlayerCore.GetComponent<ChangeActiveInterface>().ChangeActive();
        RevolutionCore.GetComponent<ChangeActiveInterface>().ChangeActive();
        Comet.GetComponent<ChangeActiveInterface>().ChangeActive();
    }
}