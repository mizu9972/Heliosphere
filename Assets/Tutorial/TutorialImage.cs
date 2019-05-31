using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TutorialImage : MonoBehaviour
{
    [Header("1つ前のページ")]
    public GameObject BeforeTutorial;
    [Header("1つ次のページ")]
    public GameObject NextTutorial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace) || Input.GetButtonDown("Cancel"))
        {
            //一つ前のページがあれば
            if(BeforeTutorial!=null)
            {
                //一つ前のページへ
                BeforeTutorial.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("action1"))
        {
            //次のページがあれば
            if(NextTutorial!=null)
            {
                //次のページへ
                NextTutorial.SetActive(true);
                this.gameObject.SetActive(false);
            }
            else//次のページがなければ
            {
                //ステージセレクトへ
                SceneManager.LoadScene("StageSelectScene");
            }
        }
    }
    bool GetBeforeKeyDown()//戻るボタンが押されたのを取得
    {
        Debug.Log("beforedown");
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            return true;
        }
        else
        {
            return true;
        }
    }
    bool GetNextKeyDown()//次へボタンが押されたのを取得
    {
        Debug.Log("nextdown");
        if (Input.GetKeyDown(KeyCode.Return))
        {
            return true;
        }
        else
        {
            return true;
        }
    }
}
