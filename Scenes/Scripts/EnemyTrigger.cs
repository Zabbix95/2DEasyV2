using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerBellyful player))
        {            
            player.SatietyChanged(true);            
            Destroy(gameObject);
        }
    }
}
