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
        //cache
        bool canInteract = false;
        

        private void Start()
        {
            if (invisible) //trigger object invisble when game starts
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        private void Update()
        {
            if (canInteract) //This is confined in update so that the keypress registers properly. may need to improve code later.
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("buttonpressed");
                    sound[0].Play();
                    TriggerDialogue();

                }
            }
        }
        //Use trigger to proc dialogue
        private void OnTriggerEnter(Collider other)
        {
            if (triggerOn && other.CompareTag("Player"))
            {
                sound[1].Play();
                TriggerDialogue();           
            }
        }

        private void OnTriggerStay(Collider other)
        {
            //checks if player can interact with object
            if (!_UI2.dM.IsDialogueActive() && interactNPC && other.CompareTag("Player")) //prevent spamming
            {
                canInteract = true;
                _UI2.charUI.InteractionText_Enable("Press 'E' to Talk");             
            }
            else
            {
                canInteract = false;
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

        //call function for dialogue
        public void TriggerDialogue()
        {
            Debug.Log("Dialoguetriggered");
            _UI2.dM.StartDialogue(dialogue);
        }

    }
}


