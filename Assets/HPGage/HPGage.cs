using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPGage : MonoBehaviour
{
    
    public GameObject Parent;
    //テクスチャを割り当てるサイズ
    private float TexWidth = 1.0f;
    private float TexHeight = 1.0f;
    private bool SwitchFlg = false;//フィーバー中か
    private RawImage MyImage;
    private float U, V;
    private float Persent;
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
        Persent=Parent.GetComponent<HPGageParent>().GetPersent();
        if (Persent>=0.5f)
        {
            //緑
            U = 1.0f;
            TexWidth = 0.5f;
        }
        else if(Persent>=0.25)
        {
            //黄色
            U = 0.25f;
            TexWidth = 0.25f;
        }
        else
        {
            //赤色
            U = 0f;
            TexWidth = 0.25f;
        }
        //TexWidthに倍率をかける
        //TexHeight = 1.0f * Parent.GetComponent<HPGageParent>().GetPersent();
        MyImage.uvRect = new Rect(U, V, TexWidth, TexHeight);
    }
    public float GaugeDivision(float Mol, float Denom)//第一引数:分子、第二引数:分母
    {
        float ReturnNum = Mol / Denom;
        return ReturnNum;
    }
}
