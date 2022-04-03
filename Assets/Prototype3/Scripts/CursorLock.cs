using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FPSMode();
    }

    private static void FPSMode()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        FPSMode(); 
    }

}
