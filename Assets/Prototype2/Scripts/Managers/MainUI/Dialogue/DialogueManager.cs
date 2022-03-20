using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Prototype2
{
    public class DialogueManager : GameBehaviour
    {
        //Dialogue sentences stored in queue
        private Queue<string> sentences;
        //Animation
        public Animator animator;
        //references
        public TMP_Text nameText;
        public TMP_Text sentenceText;
        //checks
        bool dialogueActive = false;

        void Start()
        {
            sentences = new Queue<string>();
        }
        private void Update()
        {
            if (dialogueActive)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    DisplayNextSentence();
                }
            }
        }
        //Starts the Dialogue
        public void StartDialogue(Dialogue dialogue)
        {
            _UI2.charRef.PauseCharacterInput(); //stops playermovement when reading dialogue
            _UI2.charUI.InteractionText_Disable();
            dialogueActive = true;
            animator.SetBool("isActive", true);
            nameText.text = dialogue.name;
            //Clears dialogue from the previous conversation
            sentences.Clear();
            //goes through the passed dialogue and adds them to the queue
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            //Display First sentence
            DisplayNextSentence();
        }
        //display next sentence in queue
        public void DisplayNextSentence()
        {
            animator.SetTrigger("Press");
            //Ends dialogue when no sentences left in queue
            if (sentences.Count == 0)
            {
                EndDialogue();
                return; //ends the function here instead of reading the rest
            }
            string sentence = sentences.Dequeue();      //Removes the sentence from the queue 
            StopAllCoroutines();                        //Stops all animations before displaying the next sentence(prevents spam)
            StartCoroutine(TypeSentence(sentence));     //Displays the Sentences
        }
        //Types out the sentence instead of the words being instantly displayed
        IEnumerator TypeSentence(string sentence)
        {
            //Dialogue is first set to an empty string before displaying the letters
            sentenceText.text = "";
            //loops through each character in the text(special character array function
            foreach (char letter in sentence.ToCharArray())
            {
                sentenceText.text += letter;    //displays a letter
                yield return null;              //This delays a single frame
            }
        }
        //Closes dialogue
        void EndDialogue()
        {
            animator.SetBool("isActive", false);
            dialogueActive = false;
            _UI2.charRef.PauseCharacterInput();
        }

        public bool DialogueActive()
        {
            return dialogueActive;
        }
    }

}
