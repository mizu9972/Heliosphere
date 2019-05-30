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
        //TexWidthに倍率をかける
        TexWidth = 1.0f * (float)Parent.GetComponent<HPGageParent>().GetPersent();
        MyImage.uvRect = new Rect(U, V, TexWidth, TexHeight);
    }
    public float GaugeDivision(float Mol, float Denom)//第一引数:分子、第二引数:分母
    {
        float ReturnNum = Mol / Denom;
        return ReturnNum;
    }
}
