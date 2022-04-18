using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class DialogueTrigger4 : GameBehaviour
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
                    canInteract = false;
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
                TriggerDialogue();           
            }
        }

        private void OnTriggerStay(Collider other)
        {
            //checks if player can interact with object
            if (!_UI4.dM.IsDialogueActive() && interactNPC && other.CompareTag("Player")) //prevent spamming
            {
                canInteract = true;
                _UI4.charUI.InteractionText_Enable("Press 'E' to Talk");             
            }
            else
            {
                canInteract = false;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            //disable and exit interaction
            canInteract = false;
            _UI4.charUI.InteractionText_Disable();
            //destroy trigger if bool is checked
            if (destroyOnCompletion)
            {
                Destroy(this.gameObject);
            }
        }

        //call function for dialogue
        public void TriggerDialogue()
        {
            _UI4.dM.StartDialogue(dialogue);
        }
    }


