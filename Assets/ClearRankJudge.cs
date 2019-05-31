using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ClearRankJudge : MonoBehaviour
{

    [Header("各ランクのボーダーライン(％)")]
    [SerializeField,Header("Parfect")]
    float Parfect_ = 100;
    [SerializeField, Header("Marvelous")]
    float MarVelous_ = 80;
    [SerializeField, Header("Good")]
    float Good_ = 30;

    [Header("最終評価ボーダーライン")]
    [SerializeField, Header("S")]
    float SBorder;
    [SerializeField, Header("A")]
    float ABorder;
    [SerializeField, Header("B")]
    float BBorder;
    [SerializeField, Header("C")]
    float CBorder;
    [SerializeField, Header("D")]
    float DBorder;
    [SerializeField, Header("E")]
    float EBorder;

    [Header("各評価ポイント")]

    [SerializeField, Header("Parfect")]
    float ParfectPoint = 2;
    [SerializeField, Header("Marvelous")]
    float MarVelousPoint = 1;
    [SerializeField, Header("Good")]
    float GoodPoint = 0;
    [SerializeField, Header("Bad")]
    float BadPoint = -1;

    public float MyRankPonit = -2;//最終ランク描画用

    public float ScoreSpecific = 5000;
    [SerializeField]
    GameObject ParfectImage, MarvelousImage, GoodImage, BadImage;

    [SerializeField]
    GameObject S, A, B, C, D, E;

    [SerializeField, Header("描画時間")]
    float ImageTime = 0.5f;

    enum Rank
    {
        Parfect,
        Marvelous,
        Good,
        Bad
    }

    enum LastRank
    {
        S,A,B,C,D,E
    }

    LastRank MyLastRank;
    Rank WAVEClearRank;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setLivingFriendPercent(float LivingByAll)
    {
        LivingByAll *= 100;
        if(LivingByAll >= Parfect_)
        {
            WAVEClearRank = Rank.Parfect;
        }else if(LivingByAll >= MarVelous_)
        {
            WAVEClearRank = Rank.Marvelous;
        }else if(LivingByAll >= Good_)
        {
            WAVEClearRank = Rank.Good;
        }else
        {
            WAVEClearRank = Rank.Bad;
        }

        PopRankTex();

    }

    void PopRankTex()
    {
        switch (WAVEClearRank)
        {
            case Rank.Parfect:
                ParfectImage.SetActive(true);
                Observable.Timer(System.TimeSpan.FromSeconds(ImageTime)).Subscribe(_ => ParfectImage.SetActive(false));

                MyRankPonit += ParfectPoint;
                break;

            case Rank.Marvelous:
                MarvelousImage.SetActive(true);
                Observable.Timer(System.TimeSpan.FromSeconds(ImageTime)).Subscribe(_ => MarvelousImage.SetActive(false));

                MyRankPonit += MarVelousPoint;

                break;

            case Rank.Good:
                GoodImage.SetActive(true);
                Observable.Timer(System.TimeSpan.FromSeconds(ImageTime)).Subscribe(_ => GoodImage.SetActive(false));

                MyRankPonit += GoodPoint;
                break;

            case Rank.Bad:
                BadImage.SetActive(true);
                Observable.Timer(System.TimeSpan.FromSeconds(ImageTime)).Subscribe(_ => BadImage.SetActive(false));

                MyRankPonit += BadPoint;
                break;
        }
    }

    public void RankJudge()
    {
        double NowScore = this.GetComponent<Score>().ScoreGetter();
        double JudgeSoce = NowScore + MyRankPonit * ScoreSpecific;



        if (JudgeSoce >= SBorder)
        {
            MyLastRank = LastRank.S;
        }else if(JudgeSoce >= ABorder)
        {
            MyLastRank = LastRank.A;
        }else if(JudgeSoce >= BBorder)
        {
            MyLastRank = LastRank.B;
        }else if(JudgeSoce >= CBorder)
        {
            MyLastRank = LastRank.C;
        }else if(JudgeSoce >= DBorder)
        {
            MyLastRank = LastRank.D;
        }
        else
        {
            MyLastRank = LastRank.E;
        }

        //描画
        ClearRankDraw();
    }

    void ClearRankDraw()
    {
        switch (MyLastRank)
        {
            case LastRank.S:
                S.SetActive(true);
                break;

            case LastRank.A:
                A.SetActive(true);
                break;

            case LastRank.B:
                B.SetActive(true);
                break;

            case LastRank.C:
                C.SetActive(true);
                break;

            case LastRank.D:
                D.SetActive(true);
                break;

            case LastRank.E:
                E.SetActive(true);
                break;
        }
    }
}
