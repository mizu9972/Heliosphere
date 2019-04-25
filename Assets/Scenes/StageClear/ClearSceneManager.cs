using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            //エンターキーでシーン遷移(仮)
            SceneChange();
        }
    }
    private void SceneChange()
    {
        //シーン遷移
        SceneManager.LoadScene("StageSelectScene");
    }
}
