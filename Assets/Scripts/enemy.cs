using UnityEngine;

public class enemy : MonoBehaviour
{
    Damageable damageable;

    [Header("Enemy Config")]
    [SerializeField] private float _enemyHealth = 100f;

    private void Start()
    {
         damageable = GetComponent<Damageable>();

        damageable.OnRecieveDamage += RecieveDamage;
    }

    public void RecieveDamage(float damage)
    {
        _enemyHealth -= damage;
        if (_enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
