using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FeverGauge : MonoBehaviour
{
    //テクスチャを割り当てるサイズ
    public float TexWidth = 1.0f;
    public float TexHeight = 0.5f;

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
        MyImage.uvRect = new Rect(U, V, TexWidth, TexHeight);
    }
    public float GaugeDivision(float Mol,float Denom)//第一引数:分子、第二引数:分母
    {
        float ReturnNum = Mol / Denom;
        return ReturnNum;
    }
    public void SwithGauge()//フィーバー開始終了で切り替え
    {
        SwitchFlg = !SwitchFlg;
        Debug.Log("フラグ切り替え完了" + SwitchFlg);
    }
}
