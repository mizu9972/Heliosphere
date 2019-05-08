using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BeforScene : MonoBehaviour
{ 
    
    public string SceneName;

    
    public string NamePush()
    {
        Debug.Log("Pushしたよ");
        if (SceneName != null)
        {
            return SceneName;
        }
        else
        {
            return null;
        }
    }
    public void ExportSceneName(Scene scene)
    {
        //ステージが破棄されるときにシーン名を取得する
        SceneName = scene.name;
        Debug.Log("シーン名を取得しました");
    }
}
