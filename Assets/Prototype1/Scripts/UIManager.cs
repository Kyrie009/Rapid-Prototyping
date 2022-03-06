using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Prototype1
{
    public class UIManager : GameBehaviour<UIManager>
    {
        public TMP_Text scoreText;
        public TMP_Text bestTimeText;
        public TMP_Text currentTimeText;
        public UINavigation uiNavigation;
        public Scoring scoring;

        private void Start()
        {
            //initialise references
            uiNavigation = FindObjectOfType<UINavigation>();
            scoring = FindObjectOfType<Scoring>();
        }
        public void UpdateScore(int _score)
        {
            scoreText.text = "Score: " + _score; 
        }

        public void UpdateCurrentTime(float _time)
        {
            currentTimeText.text = "Current time: " + _time.ToString("F3");
        }

        public void UpdateBestTime(float _time, bool _firstTime)
        {
            if (_firstTime)
                bestTimeText.text = "0.00";
            else
                bestTimeText.text = _time.ToString("F3");

        }
    }

}

