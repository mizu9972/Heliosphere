using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVEdit : MonoBehaviour
{
    //テクスチャの表示範囲の幅
    public float TexWidth;
    public float TexHeight;

    //UV座標変更用の変数
    private float U;
    private float V;
    
    private UnityEngine.UI.RawImage Image;

    private bool TexFlg = true;
    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントの取得
        Image = GetComponent<UnityEngine.UI.RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Image.texture != null)//テクスチャがセットされていれば
        {
            KeyInput();
            SetUV();
            SetuvRect();
        }
    }
    void KeyInput()//キー入力関係
    {
        if (Input.GetKeyDown(KeyCode.Return))//ここに選択された時の処理を記入(今は仮でEnterキーを押したら。)
        {
            TexFlg = !TexFlg;//テクスチャ読み込み位置切り替え
        }
    }
    void SetUV()//UV座標をセット
    {
        if (TexFlg)
        {
            U = 0;
            V = 0.5f;
        }
        else
        {
            U = 0;
            V = 0;
        }
    }
    void SetuvRect()//テクスチャの表示位置のセット
    {
        Image.uvRect = new Rect(U, V, TexWidth, TexHeight);//UVRectの値を設定(表示位置x,y,表示する幅w,h)
    }
}
