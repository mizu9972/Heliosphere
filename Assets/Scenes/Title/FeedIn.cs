using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
public class FeedIn : MonoBehaviour
{
    private float R, G, B;//RGB値
    private float SaveAlpha;//現在のアルファ値保存用
    private bool isActive = false;//実行中かどうか
    private float subAlpha;//変化量
    private float FeedInSpeed;//フェードインの速度
    enum Feed
    {
        In,
        Out,
    }
    private Feed isFeedInorOut;//設定がフェードインかフェードアウトか判定保存用
    [SerializeField, Header("開始待機時間")]
    float StartWaitTime;
    [SerializeField,Header("フェードインの速度")]
    float FeedInTime;
    [SerializeField, Header("初期アルファ値")]
    float StartAlpha;
    [SerializeField, Header("目標アルファ値")]
    float EndAlpha;

    private float _StartAlpha, _EndAlpha;//アルファ値を0~1の範囲に変換して保存

    // Start is called before the first frame update
    void Start()
    {

        _StartAlpha = StartAlpha / 255;
        _EndAlpha = EndAlpha / 255;
        SaveAlpha = _StartAlpha;//SaveAlpha初期化
        subAlpha = _EndAlpha - _StartAlpha;
        FeedInSpeed = subAlpha / FeedInTime;

        //StartAlphaとEndAlphaの値の上下関係でフェードインかフェードアウトか判定
        if(StartAlpha < EndAlpha)
        {
            isFeedInorOut = Feed.In;
        }else if(StartAlpha > EndAlpha)
        {
            isFeedInorOut = Feed.Out;
        }else
        {
            //同じなら実行終了
            isActive = false;
        }

        this.UpdateAsObservable()
            .Subscribe(_ => SetColor());//アルファ値適用

        //実行中かどうかの判定================================================================
        var FeedInJudgeActive = this.UpdateAsObservable()//・・・・ストリーム１
            .Where(_ => isFeedInorOut == Feed.In)//フェードインの場合
            .Where(_ => SaveAlpha >= EndAlpha);//StartAlphaが増加してEndAlpha以上

        var FeedOutJudgeActive = this.UpdateAsObservable()//・・・・ストリーム２
            .Where(_ => isFeedInorOut == Feed.Out)//フェードアウトの場合
            .Where(_ => SaveAlpha <= EndAlpha);//StartAlphaが減少してEndAlpha以下


        Observable.Amb(FeedInJudgeActive,FeedOutJudgeActive)//ストリーム１，２どちらかから流れてきたら
            .Take(1)//一回のみ
            .Subscribe(_ => isActive = false);//実行を停止
        //====================================================================================

        Observable.Timer(System.TimeSpan.FromSeconds(StartWaitTime))//待機時間秒後
            .Subscribe(_ => isActive = true);//実行中に

        //実行
        this.UpdateAsObservable()
            .Where(_ => isActive)
            .Subscribe(_ => SaveAlpha += FeedInSpeed * Time.deltaTime);//アルファ値操作
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(SaveAlpha);
    }

    void SetColor()
    {
        //アルファ値適用
        GetComponent<Image>().color = new Color(R, G, B, SaveAlpha);

    }

}
