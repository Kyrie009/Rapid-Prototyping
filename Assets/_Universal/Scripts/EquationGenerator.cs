using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquationGenerator : MonoBehaviour
{
    public enum Difficulty { EASY, MEDIUM, HARD }

    public Difficulty difficulty;

    public int numberOne;
    public int numberTwo;
    public int correctAnswer;

    public List<int> dummyAnswers;
    //references
    [HideInInspector]public string mathSymbol;

    void Update()
    {
        //DebugInputs();
    }
    public void GenerateRandomEquation()
    {
        int rnd = Random.Range(1, 100);
        if (rnd <= 35)
            GenerateAddition();
        else if (rnd <= 65)
            GenerateSubtraction();
        else 
            GenerateMultiplication(); //the division was removes since it wasn't viable
        //else
            //GenerateDivision();
    }

    void GenerateMultiplication()
    {
        numberOne = GetRandomNumbers();
        numberTwo = GetRandomNumbers();
        correctAnswer = numberOne * numberTwo;
        mathSymbol = "x";

        Debug.Log(numberOne + " x " + numberTwo + " = " + correctAnswer);

        GenerateDummyAnswers();
    }

    void GenerateAddition()
    {
        numberOne = GetRandomNumbers();
        numberTwo = GetRandomNumbers();
        correctAnswer = numberOne + numberTwo;
        mathSymbol = "+";

        Debug.Log(numberOne + " + " + numberTwo + " = " + correctAnswer);

        GenerateDummyAnswers();
    }

    void GenerateSubtraction()
    {
        numberOne = GetRandomNumbers();
        numberTwo = GetRandomNumbers();
        correctAnswer = numberOne - numberTwo;
        mathSymbol = "-";

        Debug.Log(numberOne + " - " + numberTwo + " = " + correctAnswer);

        GenerateDummyAnswers();
    }

    void GenerateDivision()
    {
        numberOne = GetRandomNumbers();
        numberTwo = GetRandomNumbers();
        correctAnswer = numberOne / numberTwo;
        correctAnswer = Mathf.RoundToInt(correctAnswer);
        mathSymbol = "/";

        Debug.Log(numberOne + " / " + numberTwo + " = " + correctAnswer);
        GenerateDummyAnswers();
    }

    /// <summary>
    /// Gets a random number based on our difficulty
    /// </summary>
    /// <returns>A random number</returns>
    int GetRandomNumbers()
    {
        switch (difficulty)
        {
            case Difficulty.EASY:
                return (Random.Range(1, 10));
            case Difficulty.MEDIUM:
                return (Random.Range(1, 20));
            case Difficulty.HARD:
                return (Random.Range(1, 100));
            default:
                return (Random.Range(1, 10));
        }
    }

    /// <summary>
    /// This will generate a set of dummy answers
    /// </summary>
    private void GenerateDummyAnswers()
    {
        for (int i = 0; i < dummyAnswers.Count; i++)
        {
            int dummy;
            do
            {
                dummy = Random.Range(correctAnswer - 10, correctAnswer + 10);
            }
            while (dummy == correctAnswer || dummyAnswers.Contains(dummy));
            dummyAnswers[i] = dummy;
            Debug.Log("Dummy answer: " + dummyAnswers[i]);
        }
    }
    public void DebugInputs()
    {
        if (Input.GetKeyDown(KeyCode.M))
            GenerateMultiplication();
        if (Input.GetKeyDown(KeyCode.A))
            GenerateAddition();
        if (Input.GetKeyDown(KeyCode.D))
            GenerateDivision();

        if (Input.GetKeyDown(KeyCode.R))
            GenerateRandomEquation();
    }

    public void GetEasy()
    {
        difficulty = Difficulty.EASY;
    }
    public void GetMedium()
    {
        difficulty = Difficulty.MEDIUM;
    }
    public void GetHard()
    {
        difficulty = Difficulty.HARD;
    }
}