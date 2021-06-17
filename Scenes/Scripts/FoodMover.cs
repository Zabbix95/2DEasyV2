using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMover : MonoBehaviour
{
    [SerializeField] private AnimationCurve _positionCurve;
    [SerializeField] private float _scale = 1f;
    
    private void FixedUpdate()
    {
        transform.position += transform.up * _positionCurve.Evaluate(Mathf.Repeat(Time.time, 1f)) * Time.deltaTime * _scale;
    }
}
