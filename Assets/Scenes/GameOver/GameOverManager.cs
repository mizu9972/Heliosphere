using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        //前のシーンを取得
        //Name=GameObject.Find("Manager").GetComponent<BeforScene>().SceneName;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return)|| Input.GetButtonDown("action1"))
        {
            //エンターキーでシーン遷移(仮)
            SceneChange();
        }
    }
    private void SceneChange()
    {
        //シーン遷移
        //SceneManager.LoadScene("StageSelectScene");
    }
}
