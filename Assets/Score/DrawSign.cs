using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DrawSign : MonoBehaviour
{
    [SerializeField]
    GameObject Parent;
    private double Score;
    private RawImage rawImage;
    private float U, V;
    //テクスチャを割り当てるサイズ
    private float TexWidth = 1.0f;
    private float TexHeight = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rawImage = this.GetComponent<RawImage>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Score = Parent.GetComponent<AddScore>().GetScore();
        if(Score>0)
        {
            //＋
            U = 0;
            V = 0.5f;
        }
        else if(Score<0)
        {
            //マイナス
            U = 0;
            V = 0;
        }
        rawImage.uvRect = new Rect(U, V, TexWidth, TexHeight);
    }
}
