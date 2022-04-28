using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Regdoll : MonoBehaviour
{
    private Damageable _damageable;

    private Rigidbody[] _rbs;
    private Collider[] _colliders;

    private Animator _animator;
    private ThirdPersonUserControl _controller;
    private CharacterController _characterController;

    [Header("Enemy Config")]
    [SerializeField] public float _enemyHealth = 100f;

    [SerializeField] private float _killForce = 5f;


    private void Awake()
    {
        _damageable = GetComponent<Damageable>();
        _animator = GetComponent<Animator>();
        _rbs = GetComponentsInChildren<Rigidbody>();
        _colliders = GetComponentsInChildren<Collider>();
        _controller = GetComponent<ThirdPersonUserControl>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _damageable.OnRecieveDamage += RecieveDamage;
        Revive();
    }

    public void RecieveDamage(float damage)
    {
        _enemyHealth -= damage;
        if (_enemyHealth <= 0)
        {
            Kill();
        }
    }


    private void Kill()
    {
        SetRegDoll(true);
        SetMain(false);
    }

    private void Revive()
    {
        SetRegDoll(false);
        SetMain(true);
    }


    void SetRegDoll(bool active)
    {
        for (int i = 0; i < _rbs.Length; i++)
        {
            _rbs[i].isKinematic = !active;

            if (active)
            {
                _rbs[i].AddForce(Vector3.forward * _killForce, ForceMode.Impulse);
            }
        }

        for (int i = 0; i < _colliders.Length; i++)
        {
            _colliders[i].enabled = active;
        }
    }

    void SetMain(bool active)
    {
        _animator.enabled = active;
        _characterController.enabled = active;
        _controller.enabled = active;
        _rbs[0].isKinematic = !active;
        _colliders[0].enabled = active;
    }


}
