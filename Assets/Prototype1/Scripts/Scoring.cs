using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype1
{
    public class Scoring : GameBehaviour
    {
        public int score;
        public float bestTime;
        public float currentTime;
        Timer timer;

        void Start()
        {
            Setup();
        }
        void Update()
        {
            if (timer.IsTiming())
            {
                _UI.UpdateCurrentTime(timer.GetTime());
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                GameOver();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                PlayerPrefs.DeleteAll();
            }
        }
        public void Setup()
        {
            if (PlayerPrefs.HasKey("BestTime"))
            {
                bestTime = PlayerPrefs.GetFloat("BestTime");
                _UI.UpdateBestTime(bestTime, false);
            }
            else
            {
                _UI.UpdateBestTime(bestTime, true);
            }
            timer = FindObjectOfType<Timer>();
            timer.StartTimer();
        }

        public void GetScore(int _score)
        {
            score += _score;
            _UI.UpdateScore(score);
        }

        public void GameOver()
        {
            timer.StopTimer();
            currentTime = timer.GetTime();

            if (currentTime > bestTime)
            {
                print("New Best Score");
                bestTime = currentTime;
                PlayerPrefs.SetFloat("BestTime", bestTime);
                _UI.UpdateBestTime(bestTime, false);
            }
        }
        //reset this games keys
        public void ResetSaveData()
        {
            PlayerPrefs.DeleteKey("BestTime");
            Setup();
        }

    }

}
