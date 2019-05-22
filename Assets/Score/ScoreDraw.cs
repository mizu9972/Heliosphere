using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreDraw : MonoBehaviour
{
    [Header("何桁目か1～5")]
    public int Dig;//桁数
    public Canvas Parent;
    private int Num;//表示する数字
    private RawImage rawImage;
    private float U, V;
    //テクスチャを割り当てるサイズ
    private float TexWidth = 0.2f;
    private float TexHeight = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        rawImage = this.GetComponent<RawImage>();
        if(Dig<1||Dig>5)//Digの値が1以上5未満でなければ警告の表示
        {
            Debug.Log("Digの値を確認してください");
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetTexturePosition();//テクスチャに座標をセット
    }
    void SetTexturePosition()
    {
        if (Parent != null)
        {
            Num = Parent.GetComponent<Score>().GetScoreDigit(Dig);//自分の桁の数字を取得
            if(Num<5)
            {
                V = Num / 5 * 0.5f + 0.5f;
            }else
            {
                V = Num / 5 * 0.5f - 0.5f;
            }
            U = Num % 5 * 0.2f;
            
            rawImage.uvRect = new Rect(U, V, TexWidth, TexHeight);
        }
    }
}
