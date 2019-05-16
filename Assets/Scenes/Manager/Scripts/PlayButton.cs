using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayButton : MonoBehaviour,IOnSelected
{
    [SerializeField, Header("オプションボタン全体")]
    GameObject Option;

    Selectable Me;

    public void OnClick()
    {
        //Managerにオプションボタン終了を通知
        GameObject.Find("Manager").GetComponent<Manager>().ChengeOptionMode();
        //ボタンが押されたら
        EventSystem.current.SetSelectedGameObject(null);//選択解除
        Option.SetActive(false);
    }

    public void OnSelected()
    {
        Me = GetComponent<Selectable>();
        //選択状態にする
        Me.Select();
    }
}
