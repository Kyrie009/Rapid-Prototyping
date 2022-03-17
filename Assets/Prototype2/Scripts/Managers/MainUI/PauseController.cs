using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype2
{
    public class PauseController : MonoBehaviour
    {
        public GameObject pausePanel;
        bool paused = false;
        void Start()
        {
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
            paused = !paused;
            if (paused == true)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            pausePanel.SetActive(paused);
            Time.timeScale = paused ? 0 : 1; //an if and else statement in 1. where "?" is the if and ":" is the els
        }
    }
}
