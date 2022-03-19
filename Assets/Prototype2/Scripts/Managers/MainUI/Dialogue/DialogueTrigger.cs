using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype2
{
    public class DialogueTrigger : GameBehaviour
    {
        public bool invisible = false;
        public bool triggerOn = false;
        public bool interactNPC = false;
        public bool destroyOnCompletion = false;
        public Dialogue dialogue;
        public AudioSource soundEffect;

        private void Start()
        {
            if (invisible)
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        //Use trigger to proc dialogue
        private void OnTriggerEnter(Collider other)
        {
            if (triggerOn && other.CompareTag("Player"))
            {
              OpenDialogue();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (interactNPC && other.CompareTag("Player"))
            {
                _UI2.charUI.InteractionText_Enable("Press 'E' to Talk");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    OpenDialogue();
                }
            }
        }

        private void OpenDialogue()
        {
            TriggerDialogue();

            if (soundEffect != null)
            {
                soundEffect.Play();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _UI2.charUI.InteractionText_Disable();
            if (destroyOnCompletion)
            {
                Destroy(this.gameObject);
            }
        }

        //call function for dialogue
        public void TriggerDialogue()
        {
            _UI2.dM.StartDialogue(dialogue);
        }

    }
}


