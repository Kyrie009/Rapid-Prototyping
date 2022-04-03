using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//This is essentially the GameManager+UI
namespace Prototype3
{
    public class UIManager : GameBehaviour<UIManager>
    {
        //Main referencehub in place of the singleton
        public CharacterUIBehavior charUI;
        public DialogueManager dM;
        public Animator hudAnim;

        //UI Objects
        public GameObject pausePanel;
        public GameObject gameOverPanel;
        bool paused = false;

        //cache
        public AudioSource button;
       
        void Start()
        {
            //Get references
            hudAnim = GetComponent<Animator>();
            charUI = FindObjectOfType<CharacterUIBehavior>();
            dM = FindObjectOfType<DialogueManager>();

            //Find UI elements
            pausePanel = GameObject.Find("PausePanel");
            gameOverPanel = GameObject.Find("GameOverPanel");

            paused = false;
            pausePanel.SetActive(false);
            gameOverPanel.SetActive(false);
            hudAnim.SetTrigger("OpeningScene");
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }
        //States
        public void Pause()
        {
            button.Play();
            PauseScene();
            pausePanel.SetActive(paused);
        }

        public void GameOver()
        {
            gameOverPanel.SetActive(true);
            hudAnim.SetTrigger("GameOver");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }


        private void PauseScene()
        {
            paused = !paused;
            if (paused == true)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            Time.timeScale = paused ? 0 : 1; //an if and else statement in 1. where "?" is the if and ":" is the else
        }
    }

}
