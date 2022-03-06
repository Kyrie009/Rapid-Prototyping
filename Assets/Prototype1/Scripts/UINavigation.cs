using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype1
{
    public class UINavigation : MonoBehaviour
    {
        public GameObject title;
        public GameObject gameMenu;
        public GameObject gameOver;
        public bool isOn;

        void Start()
        {
            //initiates references
            title = GameObject.Find("TitleScreen");
            gameMenu = GameObject.Find("GameMenu");
            gameOver = GameObject.Find("GameOver");
            //sets initial draw order
            ToggleGameMenu(false);
            ToggleGameOver(false);
            ToggleTitle(true);
        }
        //no gamestates enum setup yet so we have this jank... it works tho
        public void CheckIfOn(bool _toggle)
        {
            isOn = _toggle;
            if (isOn == true)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        public void ToggleTitle(bool _toggle)
        {           
            title.SetActive(_toggle);           
            CheckIfOn(_toggle);
        }

        public void ToggleGameMenu(bool _toggle)
        {
            gameMenu.SetActive(_toggle);
            CheckIfOn(_toggle);
        }

        public void ToggleGameOver(bool _toggle)
        {
            gameOver.SetActive(_toggle);
            CheckIfOn(_toggle);
        }

        public void ReloadScene(string _scene)
        {
            SceneManager.LoadScene(_scene);
        }  

        public void QuitGame()
        {
            Application.Quit();
        }

    }

}

