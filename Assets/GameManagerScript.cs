using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    public GameObject ReadyObject, StartObject;
    [SerializeField]
    public float ReadyTime, StartTime;

    // Start is called before the first frame update
    void Start()
    {
        ReadyObject.GetComponent<Text>().enabled = true;
        StartObject.GetComponent<Text>().enabled = false;
        Observable.Timer(System.TimeSpan.FromSeconds(ReadyTime)).Subscribe(_ => ReadyChange());
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void ReadyChange()
    {
        ReadyObject.GetComponent<Text>().enabled = false;
        StartObject.GetComponent<Text>().enabled = true;

        Observable.Timer(System.TimeSpan.FromSeconds(StartTime)).Subscribe(_ => StartChange());
    }
    private void StartChange()
    {
        StartObject.GetComponent<Text>().enabled = false;
    }
}