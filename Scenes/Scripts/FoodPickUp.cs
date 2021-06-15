using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FoodPickUp : MonoBehaviour
{
    private float _destroyDelay = 1f;
    private SpriteRenderer _sprite;
    public UnityEvent FoodPickedUp;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerBellyful player))
        {
            FoodPickedUp?.Invoke();
            player.SatietyChanged(false);
            _sprite.enabled = false;
            Destroy(gameObject, _destroyDelay);
        }
    }
}
