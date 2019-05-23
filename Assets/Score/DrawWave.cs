using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DrawWave : MonoBehaviour
{
    public int Dig;//自分の桁
    [SerializeField]
    public Canvas ParentCanvas;
    private RawImage rawImage;
    private int Num;//表示する数字
    private float U, V;
    //テクスチャを割り当てるサイズ
    private float TexWidth = 0.2f;
    private float TexHeight = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        rawImage = this.GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        DrawWaveNum();
    }
    void DrawWaveNum()
    {
        if (ParentCanvas != null)
        {
            Num = ParentCanvas.GetComponent<WaveCount>().GetNowWave(Dig);
            if (Num < 5)
            {
                V = Num / 5 * 0.5f + 0.5f;
            }
            else
            {
                V = Num / 5 * 0.5f - 0.5f;
            }
            U = Num % 5 * 0.2f;

            rawImage.uvRect = new Rect(U, V, TexWidth, TexHeight);
        }

    }
}
