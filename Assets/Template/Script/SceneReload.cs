﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneReload : MonoBehaviour
{

    public void NowSceneReload()
    {
        //自身のシーン再読み込み
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}