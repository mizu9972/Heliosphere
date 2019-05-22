using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private double Count;//スコアカウント
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ScoreCount(double value)//スコアの計算
    {
        Count += value;
        if(Count>=99999)//マックススコア
        {
            Count = 99999;
        }
    }
    public int GetScoreDigit(int Num)//何桁目を取得するかを引数で受け取る
    {
        int[] Digit=new int[5];
        Digit[0] = (int)Count % 10;//1桁
        Digit[1] = (int)Count % 100;//2桁
        Digit[2] = (int)Count % 1000;//3桁
        Digit[3] = (int)Count % 10000;//4桁
        Digit[4] = (int)Count % 100000;//5桁
        return Digit[Num-1];//引数-1の配列の要素を返す(0番目からなので)
    }
}
