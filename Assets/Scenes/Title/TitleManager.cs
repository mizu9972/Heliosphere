using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;
public class TitleManager : MonoBehaviour, IButtonPush
{
    // Start is called before the first frame update
    void Start()
    {
        this.UpdateAsObservable().Where(_ => (Input.anyKey)).Subscribe(_ => this.GetComponent<IButtonPush>().ButtonPush());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SceneChange()
    {
        //シーン遷移
        SceneManager.LoadScene("StageSelectScene");
    }

    public void ButtonPush()
    {
        //if (Input.GetKey(KeyCode.Return) || Input.GetButtonDown("action1")) 
        //{
        //    SceneChange();
        //}
        SceneChange();
    }

}
