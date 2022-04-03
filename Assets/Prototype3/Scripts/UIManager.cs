using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace Prototype3
{
    public class UIManager : GameBehaviour<UIManager>
    {
        [Header("UI")]
        public Animator animator;
        [Header("References")]
        public GameObject gameOverScreen;
        public TMP_Text helperText;
        public TMP_Text areaText;


        private void Start()
        {
            //SetDrawOrder
            gameOverScreen.SetActive(false);
            //Initialization
            OpeningScreen();
            helperText.text = "";
        }

        //UI helper text
        public void ShowHelperText(string _text)
        {
            helperText.text = _text;
        }
        
        //Scene Transitions
        public void OpeningScreen()
        {
            areaText.text = SceneManager.GetActiveScene().name;
            //animator.SetTrigger("OpeningScene");

        }
        public void TransitionScreen()
        {
            //animator.SetTrigger("Transition");
        }

    }

}
