using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerAnimationController : MonoBehaviour
{
    private PlayerMover _controller;
    private Animator _animator;
    private SpriteRenderer _body;
    private SpriteRenderer _weapon;

    private void Start()
    {
        _controller = GetComponent<PlayerMover>();     
        _animator = GetComponentInChildren<Animator>();
        _body = GetComponentInChildren<SpriteRenderer>();
        _weapon = FindObjectOfType<PlayerWeapon>().GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_controller.CurrentSpeed != 0)
            SetSpriteVelocity(_controller.CurrentSpeed);
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
}
