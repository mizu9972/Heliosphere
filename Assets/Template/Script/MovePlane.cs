using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
public class MovePlane : MonoBehaviour,ITutorial
{
    public GameObject Panel;
    
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenMoveSpace()
    {
        this.gameObject.SetActive(false);//消滅
    }
    public void StartFade()
    {
        if(Panel!=null)
        {
            Panel.GetComponent<Tutorial>().FeedOutStart();
        }
    }
}
