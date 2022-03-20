using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype2
{
    public class SceneLoader : GameBehaviour
    {
        //Sound 
        public AudioSource sound;

        public void LoadScene(string _scene)
        {
            sound.Play();
            SceneManager.LoadScene(_scene);
        }

        public void QuitGame()
        {
            sound.Play();
            Application.Quit();
        }
    }
}

