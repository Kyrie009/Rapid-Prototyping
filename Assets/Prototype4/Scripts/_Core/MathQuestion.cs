using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Prototype3;
using TMPro;

public class MathQuestion : MonoBehaviour
{
    public float answer;
    public TMP_Text answerText;
    public TMP_Text textPanel;
    public TMP_Text equationTxt;
    public GameObject[] rotatableObjects;
    // Split answer into array component
    public char[] answerChars; // stores the answer 
    // Answerlogic
    public string currentAnswerText;
    public string currentAnswer;
    public string newAnswer;
    public int yourAnswer;
    public int[] enteredNumbers; //stores the answer you entered
    public int typingCounter = -1;
    int negativeOffset = 0;
    bool answered = false;
    public bool isFirstNegative = false; // checks for negatives after first number entered
    //references
    EquationGenerator equationGenerator;

    private void Awake()
    {
        equationGenerator = FindObjectOfType<EquationGenerator>();
    }
    private void Start()
    {
        SetupEquation();
        InitiateArrays();
        ClearAnswerBoxes();
        ClearTextPanel();
    }
    //Sets up random equation equation
    public void SetupEquation()
    {
        equationGenerator.GenerateRandomEquation();
        equationTxt.text = equationGenerator.numberOne + " " + equationGenerator.mathSymbol + " " + equationGenerator.numberTwo + " = ";
        answer = equationGenerator.correctAnswer;
    }
    //AnswerCheck Logic
    public void EnterAnswer(int _number)
    {
        if (!answered)
        {
            if (typingCounter >= -1)
            {
                typingCounter++;              
                if (isFirstNegative && _number < 0)
                {
                    _number *= -1;
                    StartCoroutine(IncorrectAnswer()); //prevents incorrect format with multiple negatives in string
                }
                else
                {
                    enteredNumbers[typingCounter] = _number;
                    UpdateAnswerSlot();
                    CheckAnswer();
                }
                                                        
            }            
        }
    }
    private void CheckAnswer()
    { 
        if (yourAnswer == answer)
        {           
            StartCoroutine(CorrectAnswer());
        }
        else
        {
            if(enteredNumbers[typingCounter] == (int)Char.GetNumericValue(answerChars[typingCounter + negativeOffset])) //Coverts characters to int
            {
                isFirstNegative = true;
                return; 
            }
            if(answerChars.Length >= 2)
            {
                if (enteredNumbers[0] * -1 == (int)Char.GetNumericValue(answerChars[1])) //check for negative number since it is the first character
                {
                    negativeOffset = 1;
                    isFirstNegative = true;
                    enteredNumbers[typingCounter] = 0;
                    return;
                }
                else
                {
                    StartCoroutine(IncorrectAnswer());
                }
            }
            else
            {
                StartCoroutine(IncorrectAnswer()); //should make this more smoother in the case of spamming shots
            }

        }
    }
    IEnumerator CorrectAnswer()
    {
        textPanel.text = "Correct";
        answered = true;
        RotateAllRelatedObjects();
        yield return new WaitForSeconds(1f);
        ClearTextPanel();       
    }
    IEnumerator IncorrectAnswer()
    {
        Array.Clear(enteredNumbers, 0, enteredNumbers.Length);
        typingCounter = -1;
        isFirstNegative = false;
        negativeOffset = 0;
        textPanel.text = "Incorrect";       
        yield return new WaitForSeconds(0.2f);        
        ClearAnswerBoxes();
        yield return new WaitForSeconds(0.2f);
        ClearTextPanel();
    }
    private void ClearAnswerBoxes()
    {
        currentAnswer = "";
        newAnswer = "";
        answerText.text = ""; 
    }
    private void ClearTextPanel()
    {
        textPanel.text = "";
    }
    public void UpdateAnswerSlot()
    {
        currentAnswerText = answerText.text;
        answerText.text = currentAnswerText + enteredNumbers[typingCounter].ToString();
        //type the answer as a string then convert it to a number to check if correct;
        currentAnswer = newAnswer;
        newAnswer = currentAnswer + enteredNumbers[typingCounter].ToString();
        yourAnswer = Convert.ToInt32(newAnswer); 
    }

    private void InitiateArrays()
    {          
        answerChars = answer.ToString().ToCharArray(); //convert characters in the answer into seperate pieces
        enteredNumbers = new int[answerChars.Length]; //number of numberslots equal to answerslots      
    }
    //Triggerables
    public void RotateAllRelatedObjects()
    {
        for (int i = 0; i < rotatableObjects.Length; i++)
        {
            rotatableObjects[i].GetComponent<RotateScript>().RotateObject();
        }
    }

}

