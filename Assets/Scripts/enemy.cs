using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [Header("Enemy Config")]
    [SerializeField] public float _enemyHealth = 100;

    public void RecieveDamage(float damage)
    {
        _enemyHealth -= damage;
        if (_enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
