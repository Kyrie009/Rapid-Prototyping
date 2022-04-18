using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Prototype3;

public class UIManager4 : GameBehaviour<UIManager4>
{
    //Main referencehub in place of the singleton
    public CharacterUIBehavior charUI;
    public DialogueManager4 dM;
    public Animator hudAnim;
    public Player4 player4; //Player stats

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

    void Start()
    {
        //Get references
        hudAnim = GetComponent<Animator>();
        charUI = FindObjectOfType<CharacterUIBehavior>();
        dM = FindObjectOfType<DialogueManager4>();
        player4 = FindObjectOfType<Player4>();

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

    public void UpdatePlayerStatus() //should be better to parse in the stats rather than having the script be dependent on looking it up.
    {
        healthBar.maxValue = player4.maxHealth;
        healthBar.value = player4.health;
        healthText.text = player4.health + " / " + player4.maxHealth;
        expBar.maxValue = player4.maxExp;
        expBar.value = player4.exp;
        expText.text = "Level: " + player4.playerLevel;
        _TS.SavePlayerStatus(player4.health, player4.exp, player4.playerLevel);

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

