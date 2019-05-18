using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
public class Tutorial : MonoBehaviour
{
    private RectTransform MyTrans;
    public GameObject MovePlane;//移動範囲制限オブジェクト
    public int Nom;//PlaneNo
    // Start is called before the first frame update
    private void Awake()
    {
        if(Nom==1)
        {
            //画面の半分の割合をTOPに入れる
            MyTrans = this.GetComponent<RectTransform>();
            MyTrans.offsetMin = new Vector2(0+10.5f, Screen.height / 2.0f-9.0f-10.0f);
            MyTrans.offsetMax = new Vector2(-Screen.width / 2.0f+10.0f, 0-10.5f);
        }
        if (Nom == 2)
        {
            //画面の半分の割合をTOPに入れる
            MyTrans = this.GetComponent<RectTransform>();
            MyTrans.offsetMin = new Vector2(0 + 12.5f, Screen.height / 2.0f - 12.0f);
            MyTrans.offsetMax = new Vector2(-Screen.width / 2.0f-10.0f+12.0f,0-12.5f );

        }
        if(Nom==3)
        {
            //画面の半分の割合をTOPに入れる
            MyTrans = this.GetComponent<RectTransform>();
            MyTrans.offsetMin = new Vector2(Screen.width / 2.0f, 0);
            MyTrans.offsetMax = new Vector2(0, -Screen.height / 2.0f-14.0f);
        }
        //フェードアウトが終了したら移動範囲制限解除
        this.UpdateAsObservable().
            Where(_ => this.GetComponent<Image>().color.a <= 0&&MovePlane!=null)
            .Subscribe(_ => MovePlane.GetComponent<ITutorial>().OpenMoveSpace());
    }
    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FeedOutStart()
    {
        this.GetComponent<FeedIn>().enabled = true;
    }
}
