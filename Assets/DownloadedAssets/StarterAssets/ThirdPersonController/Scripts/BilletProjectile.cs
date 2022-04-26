using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilletProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _bulletRB;

    [Header("Bullet UI")]
    [SerializeField] private float _bulletSpeed = 100f;
    //[SerializeField] private Transform _vfxHitYes;
    //[SerializeField] private Transform _vfxHitNo;

    private void Awake()
    {
        _bulletRB = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _bulletRB.velocity = transform.forward * _bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BulletTarget>() != null)
        {
            //Instantiate(_vfxHitYes, transform.position, Quaternion.identity);
        }
        else
        {
            //Instantiate(_vfxHitNo, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
