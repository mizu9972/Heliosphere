//using System.Collections;
//using System.Collections.Generic;
//using UniRx;
//using UniRx.Triggers;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class ExitFunction : MonoBehaviour
//{
//    //ゲーム開始時(シーン読み込み前)に実行される
//    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
//    private static void LoadManagerScene()
//    {
//        string managerSceneName = "ManagerScene";

//        //ManagerSceneが有効でない時(まだ読み込んでいない時)だけ追加ロードするように
//        if (!SceneManager.GetSceneByName(managerSceneName).IsValid())
//        {
//            SceneManager.LoadScene(managerSceneName, LoadSceneMode.Additive);
//        }
//    }
//    private void Start()
//    {
//        this.UpdateAsObservable()
//            .Where(_ => Input.GetKeyDown(KeyCode.Escape))
//            .Take(1)
//            .Subscribe(_ => );
//    }
//    private void Update()
//    {
        
//    }

//}
