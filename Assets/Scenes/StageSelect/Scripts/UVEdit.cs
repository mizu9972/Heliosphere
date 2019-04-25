using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVEdit : MonoBehaviour,ISelectStage
{ 
    //テクスチャの表示範囲の幅
    public float TexWidth;
    public float TexHeight;

    //UV座標変更用の変数
    private float U = 0;
    private float V = 0.5f;

    //遷移するステージシーンの番号
    [SerializeField]
    int StageSceneNum;

    private UnityEngine.UI.RawImage Image;

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
            SetuvRect();
        }
    }

    void SetuvRect()//テクスチャの表示位置のセット
    {
        Image.uvRect = new Rect(U, V, TexWidth, TexHeight);//UVRectの値を設定(表示位置x,y,表示する幅w,h)
    }
    public void OnSelect()//選択中
    {
        Debug.Log("Hit");
        U = 0;
        V = 0;
    }
    public void RemoveSelect()//選択から外れる
    {
        Debug.Log("Remove");
        U = 0;
        V = 0.5f;
    }
}
