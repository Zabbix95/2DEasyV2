using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MinGroundNormalY = .65f;
    [SerializeField] private float GravityModifier = 1f;
    [SerializeField] private Vector2 Velocity;
    [SerializeField] private LayerMask LayerMask;

    private CapsuleCollider2D _collider;
    private SpriteRenderer _body;
    private SpriteRenderer _weapon;
    private Animator _animator;
    private Vector2 _targetVelocity;
    private bool _grounded;
    private Vector2 _groundNormal;
    private Rigidbody2D _rb2d;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);
    private Vector2 _currentNormal;
    private const float _minMoveDistance = 0.001f;
    private const float _shellRadius = 0.01f;
    private float _speedWalk = 1f;
    private float _speedRun = 2f;
    private float _currentSpeed;

    private void OnEnable()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponentInChildren<Animator>();
        _body = GetComponentInChildren<SpriteRenderer>();
        _weapon = FindObjectOfType<PlayerWeapon>().GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(LayerMask);
        _contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        _currentSpeed = SetSpeed();
        float vectorX = Input.GetAxis("Horizontal") * _currentSpeed;        
        _targetVelocity = new Vector2(vectorX, 0);

        if (Input.GetKey(KeyCode.Space) && _grounded)
            Velocity.y = 5;

        SetSpriteVelocity(vectorX);       
    }    

    private void FixedUpdate()
    {
        Velocity += GravityModifier * Physics2D.gravity * Time.deltaTime;
        Velocity.x = _targetVelocity.x;

        _grounded = false;

        Vector2 deltaPosition = Velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;        

        Movement(move, false);
     
        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    private void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > _minMoveDistance)
        {
            int count = _rb2d.Cast(move, _contactFilter, _hitBuffer, distance + _shellRadius);

            _hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                _currentNormal = _hitBufferList[i].normal;
                
                if (_currentNormal.y > MinGroundNormalY)
                {
                    _grounded = true;
                    if (yMovement)
                    {
                        _groundNormal = _currentNormal;
                        _currentNormal.x = 0;                        
                    }
                }

                float projection = Vector2.Dot(Velocity, _currentNormal);
                if (projection < 0)
                {
                    Velocity = Velocity - projection * _currentNormal;
                }

                float modifiedDistance = _hitBufferList[i].distance - _shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rb2d.position = _rb2d.position + move.normalized * distance;
    }

    private void SetSpriteVelocity(float vectorX)
    {
        if (vectorX != 0)
        {
            _body.flipX = _weapon.flipX = vectorX < 0;            
            SetAnimationClip(vectorX);
        }
    }

    private void SetAnimationClip(float vectorX)
    {
        _animator.SetFloat("HorizontalAxis", vectorX);
    }

    private float SetSpeed()
    {
        return Input.GetKey(KeyCode.LeftShift) ? _speedRun : _speedWalk;        
    }
}

