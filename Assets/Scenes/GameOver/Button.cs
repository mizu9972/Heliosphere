using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour,ISelectStage
{
    //遷移するシーンを選択する
    private string Name = null;
    //テクスチャの表示範囲の幅
    public float TexWidth;
    public float TexHeight;

    //UV座標変更用の変数
    private float U = 0;
    private float V = 0.5f;

    private UnityEngine.UI.RawImage Image;
    private GameObject gameObject;
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
        //Debug.Log("Hit");
        U = 0;
        V = 0;
    }
    public void RemoveSelect()//選択から外れる
    {
        //Debug.Log("Remove");
        U = 0;
        V = 0.5f;
    }
    public void SelectScene()
    {
        //前回のシーン名を取得
        gameObject = GameObject.Find("Manager");
        Name = gameObject.GetComponent<BeforScene>().NamePush();
        if (Name != null)
        {
            //マネージャーから取得したシーン名のシーンに遷移
            SceneManager.LoadScene(Name);
        }
        Debug.Log(Name);
    }
}
