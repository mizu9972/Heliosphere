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
    // Start is called before the first frame update
    void Start()
    {
        //画面の半分の割合をTOPに入れる
        MyTrans = GetComponent<RectTransform>();
        MyTrans.offsetMax = new Vector2(0, -Screen.height / 2.0f);
        //条件達成でフェードアウトを実行(現在は仮でSPACEキー)
        this.UpdateAsObservable().Where(_ => Input.GetKeyDown(KeyCode.Space))
            .Subscribe(_ => this.GetComponent<FeedIn>().enabled = true);
        //フェードアウトが終了したら移動範囲制限解除
        this.UpdateAsObservable().
            Where(_ => this.GetComponent<Image>().color.a <= 0&&MovePlane!=null)
            .Subscribe(_ => MovePlane.GetComponent<ITutorial>().OpenMoveSpace());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
