using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBellyful : MonoBehaviour
{
    private int _meatAmount = 0;

    public UnityAction<int> SatietyChanged;

    public void SatietyAlert(bool hungry)
    {
        _meatAmount = hungry ? --_meatAmount : ++_meatAmount;

        SatietyChanged?.Invoke(_meatAmount);
    }    
}
