using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CursorVisible : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursor;
    void Start()
    {
        UnityEngine.Cursor.visible = false;
        //UnityEngine.Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.ForceSoftware);
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        
    }
    public void OnBecameInvisible()
    {
       
    }
}
