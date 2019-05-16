using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour,IOnSelected
{
    private Button MyButton;

    Selectable Me;

    public void OnClick()
    {
        //ボタンが押されたら
        Quit();
    }

    public void Quit()
    {
        //実行環境によって終了処理を変更
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
    UnityEngine.Application.Quit();
#endif
    }

    public void OnSelected()
    {
        Me = GetComponent<Selectable>();
        //選択状態にする
        Me.Select();
    }
}
