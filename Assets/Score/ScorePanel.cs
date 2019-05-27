using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScorePanel : MonoBehaviour
{
    [Header("点滅するスピード")]
    public float BlinkSpeed = 0.01f;
    [Header("危険域スコア")]
    public double DangerScore;
    private float Alpha;
    private float R, G, B;//RGB
    private bool Switch = false;//切り替えフラグ
    private double NowScore;//現在のスコア
    [SerializeField]
    public Canvas scoreCanvas;//スコアキャンバス
    // Start is called before the first frame update
    void Start()
    {
        R = this.GetComponent<Image>().color.r;
        G = this.GetComponent<Image>().color.g;
        B = this.GetComponent<Image>().color.b;
        AlphaReset();
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreCanvas != null)
        {
            NowScore = scoreCanvas.GetComponent<Score>().ScoreGetter();//現在のスコア取得
        }
        //規定得点以下ではないならα値0
        if(NowScore>DangerScore)
        {
            AlphaReset();
        }
        else if(NowScore<DangerScore)
        {
            //規定得点以下なら点滅処理を実行
            Blink();//点滅
        }
        
        this.GetComponent<Image>().color = new Color(R, G, B, Alpha);
    }
    void Blink()
    {
        if (!Switch)//フェードアウト
        {
            Alpha -= BlinkSpeed;
        }
        else//フェードイン
        {
            Alpha += BlinkSpeed;
        }
        if (Alpha <= 0)//フェードアウトしきったらフェードインへ
        {
            Switch = true;
        }
        else if (Alpha >= 1)//フェードインしきったらフェードアウトへ
        {
            Switch = false;
        }
    }
    public void AlphaReset()
    {
        Alpha = 0;//前のフィーバーの点滅が引き継がれないようにα値をリセット
    }
}
