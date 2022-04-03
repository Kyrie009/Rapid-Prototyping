using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype3
{
    public class RotateTrigger : GameBehaviour
    {
        public GameObject[] rotatableObjects;
        bool isButtonPressed = false;
        bool canPress = true;
        public float antispamDelay = 5f;

        private void Update()
        {
            if (isButtonPressed)
            {
                isButtonPressed = false;
                RotateAllRelatedObjects();
                StartCoroutine(AntiSpamDelay());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            _UI3.charUI.InteractionText_Enable("Press E to Press Button");
        }

        private void OnTriggerStay(Collider other)
        {
            if (Input.GetKeyDown(KeyCode.E) && canPress)
            {
                canPress = false;
                isButtonPressed = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _UI3.charUI.InteractionText_Disable();
        }

        public void RotateAllRelatedObjects()
        {
            for (int i = 0; i < rotatableObjects.Length; i++)
            {
                rotatableObjects[i].GetComponent<RotateScript>().RotateObject();
            }
        }

        IEnumerator AntiSpamDelay()
        {
            yield return new WaitForSeconds(0.5f);
            canPress = true;
        }
    }
}

