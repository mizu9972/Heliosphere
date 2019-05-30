using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SelectScene : MonoBehaviour, IOnSelected
{
    [SerializeField, Header("遷移するシーン名")]
    string SceneName;

    [SerializeField, Header("オプションボタン全体(オプションのボタンでないなら設定不要)")]
    GameObject Option;

    Selectable Me;
    private GameObject SEmanager;
    void Start()
    {
        SEmanager = GameObject.Find("SEManager");
    }
    void Update()
    {
        if (SEmanager != null)
        {
            //SE再生
            if (Input.GetKeyDown(KeyCode.Return))//エンターで決定
            {
                SEmanager.GetComponent<SEManager>().PlaySE(SEManager.AudioType.Enter);
            }
            else if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Return))//エンターキー以外の入力があればカーソル移動の効果音
            {
                SEmanager.GetComponent<SEManager>().PlaySE(SEManager.AudioType.Click);
            }
        }
    }
    public void OnClick()
    {
        //シーン遷移
        SceneManager.LoadScene(SceneName);

    }

    public void OnClickbyOptionMode()
    {
        //オプション画面用
        //上手く動いてないので未実装
        Option.SetActive(false);
        SceneManager.LoadScene(SceneName);
        Debug.Log("su");

    }
    public void OnSelected()
    {
        Me = GetComponent<Selectable>();
        //選択状態にする
        Me.Select();
    }
}
