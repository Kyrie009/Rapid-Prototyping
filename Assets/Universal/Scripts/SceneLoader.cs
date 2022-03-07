using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : GameBehaviour
{
    public GameObject prototypeSelect;
    void Start()
    {
        prototypeSelect = GameObject.Find("PrototypeSelect");
        OpenProtoSelect(false);

    }

    public void OpenProtoSelect(bool _toggle)
    {
        prototypeSelect.SetActive(_toggle);

    }

    public void LoadScene(string _scene)
    {
        SceneManager.LoadScene(_scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
