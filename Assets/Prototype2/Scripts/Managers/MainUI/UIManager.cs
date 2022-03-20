using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//This is essentially the GameManager+UI
namespace Prototype2
{
    public class UIManager : GameBehaviour<UIManager>
    {
        //Main referencehub in place of the singleton
        public CharacterClass charRef;
        public CharacterStatus charStat;
        public CharacterUIBehavior charUI;
        public DialogueManager dM;
        public Animator hudAnim;

        //UI Objects
        public GameObject pausePanel;
        public GameObject gameOverPanel;
        bool paused = false;
        //PlayerUI
        public TMP_Text playerName;
        public TMP_Text healthText;
        public Slider healthBar;
        public Slider expBar;
        public TMP_Text expText;
        //cache
        public AudioSource button;

        public void UpdatePlayerStatus() //should be better to parse in the stats rather than having the script be dependent on looking it up.
        {
            healthBar.maxValue = charStat.maxHealth;
            healthBar.value = charStat.health;
            healthText.text = charStat.health + " / " + charStat.maxHealth;
            expBar.maxValue = charStat.maxExp;
            expBar.value = charStat.exp;
            expText.text = "Level: " + charStat.playerLevel;

        }
        void Start()
        {
            //Get references
            hudAnim = GetComponent<Animator>();
            charStat = FindObjectOfType<CharacterStatus>();
            charRef = FindObjectOfType<CharacterClass>();
            charUI = FindObjectOfType<CharacterUIBehavior>();
            dM = FindObjectOfType<DialogueManager>();

            //Find UI elements
            pausePanel = GameObject.Find("PausePanel");
            gameOverPanel = GameObject.Find("GameOverPanel");

            paused = false;
            pausePanel.SetActive(false);
            gameOverPanel.SetActive(false);
            Time.timeScale = 1;

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
