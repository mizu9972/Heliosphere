using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Score : MonoBehaviour
{
    private GameObject GameMaster;
    [SerializeField,Header("スコア")]
    private double Count = 1000;//スコアカウント
    private double JudgeCount;
    [SerializeField,Header("マックススコア")]
    double MaxCount = 99999;

    [SerializeField, Header("スコアアップフレーム")]
    int Frame = 10;

    public GameObject AddScore;
    private bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        GameMaster = GameObject.Find("GameMaster").gameObject;
        JudgeCount = Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ScoreCount(double value)//スコアの計算
    {
        //加減算される点数表示用のキャンバスのenabledをtrueにして点数を送信
        if (AddScore)//表示状態ならenabedをfalseに
        {
            AddScore.SetActive(false);
        }
        //獲得スコア表示のやつに数字（value）セット
        AddScore.GetComponent<AddScore>().SetAddScore(value);
        AddScore.SetActive(true);//描画
        //一定時間経過で描画終了
        Observable.Timer(System.TimeSpan.FromSeconds(1)).
            Subscribe(_ => AddScore.SetActive(false));
        JudgeCount += value;
        var Function = Observable.Interval(System.TimeSpan.FromMilliseconds(16)).Take(Frame).Where(_ =>isGameOver == false).Subscribe(_ =>Count += value / Frame);
        GameMaster.GetComponent<GameMaster>().CountUp(value);
        if(JudgeCount >= MaxCount)//マックススコア
        {
            JudgeCount = MaxCount;
        }
        if(JudgeCount <= 0)
        {
            JudgeCount = 0;
            Count = 0;

            isGameOver = true;
            GameMaster.GetComponent<GameMaster>().ToGameOver();
        }
        Debug.Log(JudgeCount);
    }

    public void FeverScoreCount(double value)
    {
        //加減算される点数表示用のキャンバスのenabledをtrueにして点数を送信
        if (AddScore)//表示状態ならenabedをfalseに
        {
            AddScore.SetActive(false);
        }
        //獲得スコア表示のやつに数字（value）セット
        AddScore.GetComponent<AddScore>().SetAddScore(value);
        AddScore.SetActive(true);//描画
        //一定時間経過で描画終了
        Observable.Timer(System.TimeSpan.FromSeconds(1)).
            Subscribe(_ => AddScore.SetActive(false));
        Debug.Log(value);
        JudgeCount += value;
        Observable.Interval(System.TimeSpan.FromMilliseconds(16)).Take(Frame).Subscribe(_ => Count += value / Frame);


        if (JudgeCount >= MaxCount)//マックススコア
        {
            JudgeCount = MaxCount;
            Count = MaxCount;
        }

    }
    public int GetScoreDigit(int Num)//何桁目を取得するかを引数で受け取る
    {
        int[] Digit=new int[5];
        Digit[0] = (int)Count % 10;//1桁
        Digit[1] = ((int)Count % 100)/10;//2桁
        Digit[2] = ((int)Count % 1000)/100;//3桁
        Digit[3] = ((int)Count % 10000)/1000;//4桁
        Digit[4] = ((int)Count % 100000)/10000;//5桁
        return Digit[Num-1];//引数-1の配列の要素を返す(0番目からなので)
    }
}
