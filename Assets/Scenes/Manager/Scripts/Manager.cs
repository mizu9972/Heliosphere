using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject gameobject;
    [SerializeField, Header("オプションボタン")]
    GameObject Option;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameobject);
    }
    private void Update()
    {
        //ESCキーが押されたら終了処理実行
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Option.SetActive(true);
        }
    }

    void Quit()
    {
        //実行環境によって終了処理を変更
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
    UnityEngine.Application.Quit();
#endif
    }
}
