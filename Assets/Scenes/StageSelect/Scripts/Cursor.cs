using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Cursor : MonoBehaviour
{
    //　アイコンが1秒間に何ピクセル移動するか
    [SerializeField]
    private float iconSpeed = Screen.width;
    //　アイコンのサイズ取得で使用
    private RectTransform rect;
    //　アイコンが画面内に収まる為のオフセット値
    private Vector2 offset;

    //選択中か
    private bool SelectFlg=false;

    
    void Start()
    {
        rect = GetComponent<RectTransform>();
        //　オフセット値をアイコンのサイズの半分で設定
        offset = new Vector2(rect.sizeDelta.x / 2f, rect.sizeDelta.y / 2f);
    }
    
    void Update()
    {
        //　移動キーを押していなければ何もしない
        //if (Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f)
        //{
        //    return;
        //}

        //　移動先を計算
        
        var pos = rect.anchoredPosition + new Vector2(Input.GetAxis("Horizontal") * iconSpeed, Input.GetAxis("Vertical") * iconSpeed) * Time.deltaTime;

        //　アイコンが画面外に出ないようにする
        pos.x = Mathf.Clamp(pos.x, -Screen.width * 0.5f + offset.x, Screen.width * 0.5f - offset.x);
        pos.y = Mathf.Clamp(pos.y, -Screen.height * 0.5f + offset.y, Screen.height * 0.5f - offset.y);
        //　アイコン位置を設定
        rect.anchoredPosition = pos;

        Debug.Log(SelectFlg);
        //Debug.Log(EnterFlg);
    }

   
    void OnTriggerStay(Collider other)//選択中
    {
        other.GetComponent<ISelectStage>().OnSelect();
        SelectFlg = true;
        if (Input.GetKey(KeyCode.Return) || Input.GetButtonDown("action1"))
        {
            //ステージアイコン選択中かつ決定ボタンが押されたら遷移
            //選択したステージへ
            
            this.UpdateAsObservable()
                .Where(_ => SelectFlg)
                .Take(1)//1回のみの処理
                .Subscribe(_ => other.GetComponent<ISelectStage>().SelectScene());

        }
    }
    void OnTriggerExit(Collider other)//離れたら
    {
        SelectFlg = false;
        other.GetComponent<ISelectStage>().RemoveSelect();
    }
}
