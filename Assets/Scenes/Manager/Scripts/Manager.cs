using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject gameobject;
    public GameObject Canvas;
    public GameObject Planetarium;
    [SerializeField, Header("オプションボタン全体")]
    GameObject Option;
    [SerializeField, Header("リザルトボタン全体")]
    GameObject Result;
    [SerializeField, Header("クリアボタン全体")]
    GameObject Clear;
    [SerializeField, Header("WAVEモードのクリア画面")]
    GameObject WAVEClear;

    [SerializeField, Header("最初に選択状態にするオプションボタン")]
    GameObject InitialOptionButton;
    [SerializeField, Header("最初に選択状態にするリザルトボタン")]
    GameObject InitialResultButton;
    [SerializeField, Header("最初に選択状態にするクリアボタン")]
    GameObject InitialClearButton;
    [SerializeField, Header("WAVE用最初に選択状態にするクリアボタン")]
    GameObject InitialWAVEClearButton;
    private bool isOptionMode = false;//実行中かどうか
    private bool isResultMode = false;

    private bool isActive = false;//実行可能かどうか

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameobject);
        DontDestroyOnLoad(Canvas);
        DontDestroyOnLoad(Planetarium);
    }
    private void Update()
    {
        //ESCキーが押されたらオプション呼び出し
        if ((Input.GetKeyDown(KeyCode.Escape)==true || Input.GetButtonDown("Option")==true) && isOptionMode == false && isActive == true && isResultMode == false)
        {
            CallOption();
        }
    }

    void CallOption()
    {
        //オプションボタン呼び出し
        ChengeOptionMode();
        var Selectable = InitialOptionButton.GetComponent<IOnSelected>();

        Option.SetActive(true);//オプション有効化

        if (Selectable != null)
        {
            //ボタン選択状態に
            InitialOptionButton.GetComponent<IOnSelected>().OnSelected();
        }


    }

    public void CallResult()
    {
        ChangeResultMode();
        var Selectable = InitialResultButton.GetComponent<IOnSelected>();

        Result.SetActive(true);//リザルト有効化

        if(Selectable != null)
        {
            //ボタン選択状態に
            InitialResultButton.GetComponent<IOnSelected>().OnSelected();
        }
    }

    public void CallClear()
    {
        ChangeResultMode();
        var Selectable = InitialClearButton.GetComponent<IOnSelected>();

        Clear.SetActive(true);


        if (Selectable != null)
        {
            //ボタン選択状態に
            InitialClearButton.GetComponent<IOnSelected>().OnSelected();
        }
    }

    public void CallWAVEClear()
    {
        ChangeResultMode();

        var Selectable = InitialWAVEClearButton.GetComponent<IOnSelected>();

        WAVEClear.SetActive(true);


        if (Selectable != null)
        {
            //ボタン選択状態に
            InitialWAVEClearButton.GetComponent<IOnSelected>().OnSelected();
        }
    }
    public void ChangeResultMode()
    {
        isResultMode = !isResultMode;
    }

    public void ChengeOptionMode()
    {
        //オプションボタン起動、終了時に呼び出す
        GameObject.Find("GameManager").GetComponent<GameManagerScript>().AllChangeActive();//オブジェクト停止・再生切り替え
        isOptionMode = !isOptionMode;
    }

    public void ChengeActive(bool _isActive)
    {
        //実行可不可切り替え
        isActive = _isActive;
    }
}
