using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Prototype2
{
    public class UIManager : GameBehaviour<UIManager>
    {
        //Main referencehub in place of the singleton
        public CharacterClass charRef;
        public CharacterStatus charStat;
        public CharacterUIBehavior charUI;
        public DialogueManager dM;

        //UI Objects
        public GameObject pausePanel;
        bool paused = false;
        //PlayerUI
        public TMP_Text playerName;
        public TMP_Text healthText;
        public Slider healthBar;
        public Slider expBar;
        public TMP_Text expText;
        bool isDialogueOpen = false;

        public void UpdatePlayerStatus()
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
            charStat = FindObjectOfType<CharacterStatus>();
            charRef = FindObjectOfType<CharacterClass>();
            charUI = FindObjectOfType<CharacterUIBehavior>();

            paused = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1;

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }

        public void Pause()
        {
            PauseScene();
            pausePanel.SetActive(paused);
        }

        public void DialoguePause()
        {
            isDialogueOpen = !isDialogueOpen;
            PauseScene();
        }

        private void PauseScene()
        {
            paused = !paused;
            if (paused == true)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            Time.timeScale = paused ? 0 : 1; //an if and else statement in 1. where "?" is the if and ":" is the else
        }
    }
}
