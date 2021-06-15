using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBellyful : MonoBehaviour
{
    private int _meatAmount = 0;

    public event UnityAction<int> SatietyAlert;

    public void SatietyChanged(bool hungry)
    {
        _meatAmount = hungry ? --_meatAmount : ++_meatAmount;
        SatietyAlert?.Invoke(_meatAmount);
    }    
}
