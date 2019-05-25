using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FeverGauge : MonoBehaviour
{
    //テクスチャを割り当てるサイズ
    public float TexWidth = 1.0f;
    public float TexHeight = 0.5f;
    public GameObject Parent;
    private bool SwitchFlg = false;//フィーバー中か
    private RawImage MyImage;
    private float U, V;
    // Start is called before the first frame update
    void Start()
    {
        MyImage = this.GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {

        SetTexPos();
    }
    void SetTexPos()
    {
        if(!SwitchFlg)
        {
            U = 0;
            V = 0.5f;
        }
        else
        {
            U = 0;
            V = 0;
        }
        //TexWidthに倍率をかける
        TexWidth = 1.0f*(float)Parent.GetComponent<GaugeParent>().GetPersent();
        MyImage.uvRect = new Rect(U, V, TexWidth, TexHeight);
    }
    public double GaugeDivision(double Mol,double Denom)//第一引数:分子、第二引数:分母
    {
        double ReturnNum = Mol / Denom;
        return ReturnNum;
    }
    public void SwithGauge(bool flg)//フィーバー開始終了で切り替え
    {
        SwitchFlg = flg;
        //Debug.Log("フラグ切り替え完了" + SwitchFlg);
    }
    public bool GetSwitchFlg()//現在フィーバーかどうかを返す(trueはフィーバー中)
    {
        return SwitchFlg;
    }
}
