using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype2
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadScene(string _scene)
        {
            SceneManager.LoadScene(_scene);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}

