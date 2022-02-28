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

        public void UpdateScore(int _score)
        {
            scoreText.text = "Score: " + _score; 
        }

        public void UpdateCurrentTime(float _time)
        {
            currentTimeText.text = "Current time: " + _time.ToString("F3");
        }

        public void UpdateBestTime(float _time, bool _firstTime = false)
        {
            if (_firstTime)
                bestTimeText.text = "Best time: 0";
            else
                bestTimeText.text = "Best time: " + _time.ToString("F3");

        }
    }

}

