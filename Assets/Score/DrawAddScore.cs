using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DrawAddScore : MonoBehaviour
{
    public GameObject Parent;
    public int Dig;//自分の桁
    
    private RawImage rawImage;
    private float U, V;
    //テクスチャを割り当てるサイズ
    private float TexWidth = 0.2f;
    private float TexHeight = 0.5f;
    private int Num;
    
    // Start is called before the first frame update
    void Start()
    {
        rawImage = this.GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        SetTexturePosition();
        rawImage.uvRect = new Rect(U, V, TexWidth, TexHeight);
    }
    
    void SetTexturePosition()
    {
        
        Num = Parent.GetComponent<AddScore>().DigitSet(Dig);//自分の桁に応じた数字の取得
        Debug.Log(Dig+"桁目＝"+Num);
        if (Num < 5)
        {
            V = Num / 5 * 0.5f + 0.5f;
        }
        else
        {
            V = Num / 5 * 0.5f - 0.5f;
        }
        U = Num % 5 * 0.2f;
    }
    
}
