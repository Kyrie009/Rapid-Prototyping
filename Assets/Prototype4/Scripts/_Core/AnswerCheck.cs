using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Prototype3;
using TMPro;

public class AnswerCheck : MonoBehaviour
{
    public TMP_Text answerBox;
    public int answer;
    string answerOutput;
    int yourAnswer;
    int firstNumber;
    int secondNumber;
    int typingCounter = 0;
    public GameObject[] rotatableObjects;
    bool isCorrect = false;
    bool answered = false;

    private void Update()
    {
        if (isCorrect)
        {
            isCorrect = false;
            answered = true;
            RotateAllRelatedObjects();
        }
    }

    public void UpdateAnswerBox()
    {
        answerBox.text = answerOutput;
    }

    public void CheckAnswer(int _number)
    {
        if (!answered)
        {
            if (typingCounter == 0)
            {
                typingCounter++;
                firstNumber = _number;
            }
            if (typingCounter == 1)
            {
                secondNumber = _number;
                UpdateAnswerOutput();
                if (yourAnswer == answer)
                {
                    isCorrect = true;
                }
                else
                {
                    typingCounter = 0;
                    answerOutput = "";
                }
            }
            UpdateAnswerOutput();
        }

    }

    private void UpdateAnswerOutput()
    {
        answerOutput = firstNumber.ToString() + secondNumber.ToString();
        yourAnswer = Convert.ToInt32(answerOutput);
        UpdateAnswerBox();
    }

    public void RotateAllRelatedObjects()
    {
        for (int i = 0; i < rotatableObjects.Length; i++)
        {
            rotatableObjects[i].GetComponent<RotateScript>().RotateObject();
        }
    }

}

