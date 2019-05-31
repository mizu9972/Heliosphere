using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RetryButton : MonoBehaviour,IOnSelected
{
    [SerializeField, Header("リザルトボタン全体")]
    GameObject Result;
    Selectable Me;
    public void OnClick()
    {
        GameObject.Find("Manager").GetComponent<Manager>().ChangeResultMode();
       
        EventSystem.current.SetSelectedGameObject(null);
        GameObject.FindWithTag("GameManager").GetComponent<SceneReload>().NowSceneReload();
        
        Result.SetActive(false);
        Debug.Log(Result);
    }

    public void OnSelected()
    {
        Me = GetComponent<Selectable>();
        //選択状態にする
        Me.Select();
    }
}
