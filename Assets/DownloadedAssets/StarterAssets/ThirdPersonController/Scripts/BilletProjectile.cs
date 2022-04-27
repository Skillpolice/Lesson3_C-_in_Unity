using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilletProjectile : MonoBehaviour
{
    enemy enemy;
    Damageable damageable;

    [SerializeField] private Rigidbody _bulletRB;

    [Header("Bullet UI")]
    [SerializeField] private float _bulletSpeed = 100f;
    [SerializeField] private float _bulletDamage = 15f;

    //[SerializeField] private Transform _vfxHitYes;
    //[SerializeField] private Transform _vfxHitNo;

    private void Awake()
    {
        enemy = FindObjectOfType<enemy>();
        _bulletRB = GetComponent<Rigidbody>();
        damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        _bulletRB.velocity = transform.forward * _bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BulletTarget>() != null)
        {
            enemy.RecieveDamage(_bulletDamage);
        }
        else
        {
            //Instantiate(_vfxHitNo, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
