using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ToSelectScene : MonoBehaviour,IOnSelected
{
    [SerializeField, Header("リザルトボタン全体")]
    GameObject Result;
    Selectable Me;
    public GameObject manager;
    public void OnClick()
    {
        GameObject.Find("Manager").GetComponent<AudioManager>().PlayClick();
        GameObject.Find("Manager").GetComponent<Manager>().ChangeResultMode();
        EventSystem.current.SetSelectedGameObject(null);

        SceneManager.LoadScene("StageSelectScene");

        Result.SetActive(false);
    }

    public void OnSelected()
    {
        Me = GetComponent<Selectable>();
        //選択状態にする
        Me.Select();
    }
}
