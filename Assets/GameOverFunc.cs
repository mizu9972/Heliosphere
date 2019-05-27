using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverFunc : MonoBehaviour
{

    public void GameOver()
    {
        this.gameObject.SetActive(false);
    }
}
