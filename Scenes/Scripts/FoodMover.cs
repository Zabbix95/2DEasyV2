using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMover : MonoBehaviour
{
    [SerializeField] private AnimationCurve positionCurve;
    [SerializeField] private float _scale = 1f;
    
    private void FixedUpdate()
    {
        transform.position += transform.up * positionCurve.Evaluate(Mathf.Repeat(Time.time, 1f)) * Time.deltaTime * _scale;
    }
}
