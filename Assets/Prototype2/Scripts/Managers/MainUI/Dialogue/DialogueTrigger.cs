using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype2
{
    public class DialogueTrigger : GameBehaviour
    {
        [Header("Trigger Conditions")]
        public bool invisible = false;
        public bool triggerOn = false;
        public bool interactNPC = false;
        public bool destroyOnCompletion = false;
        [Header("Dialogue")]
        public Dialogue dialogue;
        [Header("Sounds")]
        public AudioSource[] sound;
        

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
                sound[1].Play();
                OpenDialogue();
               
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_UI2.dM.DialogueActive() && interactNPC && other.CompareTag("Player")) //prevent spamming
            {
                _UI2.charUI.InteractionText_Enable("Press 'E' to Talk");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    sound[0].Play();
                    OpenDialogue();
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            //disable interaction text
            _UI2.charUI.InteractionText_Disable();
            //destroy object if bool is checked
            if (destroyOnCompletion)
            {
                Destroy(this.gameObject);
            }
        }
        private void OpenDialogue()
        {
            TriggerDialogue();

        }

        //call function for dialogue
        public void TriggerDialogue()
        {
            _UI2.dM.StartDialogue(dialogue);
        }

    }
}


