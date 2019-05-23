using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FeverBlink : MonoBehaviour
{
    [Header("点滅するスピード")]
    public float BlinkSpeed = 0.01f;
    private float Alpha;//現在のアルファ値
    private float R, G, B;//RGB
    private bool Switch = false;//切り替えフラグ

    // Start is called before the first frame update
    void Start()
    {
        R = GetComponent<RawImage>().color.r;
        G = GetComponent<RawImage>().color.g;
        B = GetComponent<RawImage>().color.b;
        AlphaReset();
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<RawImage>().color = new Color(R, G, B, Alpha);
        Blink();//点滅
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
        Alpha = 1;//前のフィーバーの点滅が引き継がれないようにα値をリセット
    }
}
