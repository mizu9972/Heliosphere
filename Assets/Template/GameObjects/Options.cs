using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour,IMoveOperate
{

    Button Button1;
    Button Button2;
    Button Button3;

    //ボタン数
    const int ButtonNum = 3;
    //選ばれているボタン
    private Button SelectedButton;

    //１フレーム前のボタン入力
    private bool isInputBeforeKey;

    private int selectedButtonNum = 1;
    // Start is called before the first frame update
    void Start()
    {
        Button1 = GameObject.Find("Canvas/Options/play").GetComponent<Button>();
        Button2 = GameObject.Find("Canvas/Options/volume").GetComponent<Button>();
        Button3 = GameObject.Find("Canvas/Options/quit").GetComponent<Button>();

        ButtonSelect();
    }

    // Update is called once per frame
    void Update()
    {
        //SelectedButton.OnSelect();
    }

    public void MoveControll()
    {
        //キー操作
        var PushVerticalKey = Input.GetAxis("Vertical");

        if(PushVerticalKey > 0 && isInputBeforeKey == false)
        {
            //上入力
            selectedButtonNum -= 1;
            ButtonSelect();

            isInputBeforeKey = true;

        }else if(PushVerticalKey < 0 && isInputBeforeKey == false)
        {
            //下入力
            selectedButtonNum += 1;
            ButtonSelect();

            isInputBeforeKey = true;

        } else
        {
            isInputBeforeKey = false;
        }
    }

    void ButtonSelect()
    {
        //selectedButtonNumが0以下、ButtonNumより上にならないように
        if(selectedButtonNum <= 0)
        {
            selectedButtonNum += 1;
        }else if(selectedButtonNum > ButtonNum)
        {
            selectedButtonNum -= 1;
        }

        //selectedButtonNumの数値に合わせて選ばれているボタンを切り替え
        switch (selectedButtonNum)
        {
            case 1:
                SelectedButton = Button1.GetComponent<Button>();
                Button1.OnSelect();
                break;
               
            case 2:
                SelectedButton = Button2.GetComponent<Button>();
                Button2.OnSelect();
                break;

            case 3:
                SelectedButton = Button3.GetComponent<Button>();
                Button3.OnSelect();
                break;
        }
    }
}
