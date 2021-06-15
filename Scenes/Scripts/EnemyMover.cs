using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;    

    private float RotationSpeed = 4f;
    private float AnglesToRotate = 90f;
    private Transform[] _points;
    private int _currentPoint = 1;
    private bool ReverseDirection = false;
    private int _rotationDirection = -1;
    
    private void Start()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }
   
    void Update()
    {
        Transform target = _points[_currentPoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);        
        transform.Rotate(Vector3.forward, AnglesToRotate * _rotationDirection * RotationSpeed * Time.deltaTime);

        if (transform.position == target.position)
        {
            if (_currentPoint == _points.Length - 1 || _currentPoint == 0)
            {
                ReverseDirection = !ReverseDirection;
                _rotationDirection *= -1;
            }
            
            _currentPoint = ReverseDirection? --_currentPoint: ++_currentPoint;
        }
    }
}
