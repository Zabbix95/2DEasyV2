using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;    

    private float _rotationSpeed = 4f;
    private float _anglesToRotate = 90f;
    private Transform[] _points;
    private int _currentPoint = 1;
    private bool _reverseDirection = false;
    private int _rotationDirection = -1;
    
    private void Start()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }
   
    private void Update()
    {
        Transform target = _points[_currentPoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);        
        transform.Rotate(Vector3.forward, _anglesToRotate * _rotationDirection * _rotationSpeed * Time.deltaTime);

        if (transform.position == target.position)
        {
            if (_currentPoint == _points.Length - 1 || _currentPoint == 0)
            {
                _reverseDirection = !_reverseDirection;
                _rotationDirection *= -1;
            }
            
            _currentPoint = _reverseDirection? --_currentPoint: ++_currentPoint;
        }
    }
}
