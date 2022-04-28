using UnityEngine;

public class BilletProjectile : MonoBehaviour
{
    Damageable _damageable;
    enemy enemy;
    Regdoll _regdoll;

    [SerializeField] private Rigidbody _bulletRB;

    [Header("Bullet UI")]
    [SerializeField] private float _bulletSpeed = 100f;
    [SerializeField] private float _bulletDamage = 10f;

    //[SerializeField] private Transform _vfxHitYes;
    //[SerializeField] private Transform _vfxHitNo;

    private void Awake()
    {
        _bulletRB = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        enemy = FindObjectOfType<enemy>();
        _regdoll = FindObjectOfType<Regdoll>();

        _bulletRB.velocity = transform.forward * _bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        _damageable = other.GetComponent<Damageable>();
        if (_damageable != null)
        {
            _damageable.DoDamage(_bulletDamage);
        }
        else
        {
            //Instantiate(_vfxHitNo, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

}
