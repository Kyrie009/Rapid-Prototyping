using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResonanceInputs : MonoBehaviour
{
    public int resonanceLevel = 0;
    public TMP_Text resonanceText;
    public TMP_Text chargeSymbolText;
    public Image resonancePanel;
    bool isNegative = false;
    string chargeSymbol;

    private void Start()
    {
        resonancePanel = GetComponent<Image>();
        resonanceLevel = 0;
        UpdateResonance();
    }

    void Update()
    {
        ChargeSwitchInput();
        ResonanceInput();
    }

    private void ChargeSwitchInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isNegative = !isNegative;
            resonanceLevel *= -1;
            UpdateResonance();
            if (isNegative)
            {
                chargeSymbol = "-";
                chargeSymbolText.text = chargeSymbol;
                Color negativeColor = Color.blue;
                negativeColor.a = 0.5f;
                resonancePanel.color = negativeColor;
            }
            else
            {
                chargeSymbol = "+";
                chargeSymbolText.text = chargeSymbol;
                Color positiveColor = Color.black;
                positiveColor.a = 0.5f;
                resonancePanel.color = positiveColor;
            }
        }
    }

    private void ResonanceInput()
    {
        if (isNegative)
        {
            
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                resonanceLevel++;
                if (resonanceLevel > 0)
                {
                    resonanceLevel = -9;
                }
                UpdateResonance();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                resonanceLevel--;
                if (resonanceLevel < -9)
                {
                    resonanceLevel = 0;
                }
                UpdateResonance();
            }
        }
        else
        {        
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                resonanceLevel++;
                if (resonanceLevel > 9)
                {
                    resonanceLevel = 0;
                }
                UpdateResonance();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                resonanceLevel--;
                if (resonanceLevel < 0)
                {
                    resonanceLevel = 9;
                }
                UpdateResonance();
            }
        }
    }

    private void UpdateResonance()
    {
        resonanceText.text = "Resonance: " + resonanceLevel;
    }
}
