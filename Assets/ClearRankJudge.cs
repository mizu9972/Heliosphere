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


    [SerializeField]
    GameObject ParfectImage, MarvelousImage, GoodImage, BadImage;

    [SerializeField, Header("描画時間")]
    float ImageTime = 0.5f;

    enum Rank
    {
        Parfect,
        Marvelous,
        Good,
        Bad
    }

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
                break;

            case Rank.Marvelous:
                MarvelousImage.SetActive(true);
                Observable.Timer(System.TimeSpan.FromSeconds(ImageTime)).Subscribe(_ => MarvelousImage.SetActive(false));
                break;

            case Rank.Good:
                GoodImage.SetActive(true);
                Observable.Timer(System.TimeSpan.FromSeconds(ImageTime)).Subscribe(_ => GoodImage.SetActive(false));
                break;

            case Rank.Bad:
                BadImage.SetActive(true);
                Observable.Timer(System.TimeSpan.FromSeconds(ImageTime)).Subscribe(_ => BadImage.SetActive(false));
                break;
        }
    }
}
