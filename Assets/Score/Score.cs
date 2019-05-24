using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private GameObject GameMaster;
    [SerializeField,Header("スコア")]
    private double Count = 1000;//スコアカウント
    [SerializeField,Header("マックススコア")]
    double MaxCount = 99999;
    // Start is called before the first frame update
    void Start()
    {
        GameMaster = GameObject.Find("GameMaster").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ScoreCount(double value)//スコアの計算
    {
        Debug.Log(value);
        Count += value;
        GameMaster.GetComponent<GameMaster>().CountUp(value);
        if(Count>= MaxCount)//マックススコア
        {
            Count = MaxCount;
        }
        if(Count <= 0)
        {
            Count = 0;
            GameMaster.GetComponent<GameMaster>().ToGameOver();
        }
        Debug.Log(Count);
    }

    public void FeverScoreCount(double value)
    {
        Debug.Log(value);
        Count += value;

        if (Count >= MaxCount)//マックススコア
        {
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
