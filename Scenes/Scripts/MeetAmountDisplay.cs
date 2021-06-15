using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeetAmountDisplay : MonoBehaviour
{
    [SerializeField] private PlayerBellyful _playerBellyful;
    [SerializeField] private Text _textDsiplay;

    private void OnEnable()
    {
        _playerBellyful.SatietyAlert += DisplayAmountOfMeat;
    }

    private void OnDisable()
    {
        _playerBellyful.SatietyAlert -= DisplayAmountOfMeat;
    }    

    private void DisplayAmountOfMeat(int meatAmount)
    {
        _textDsiplay.text = meatAmount.ToString();
    }    
}
