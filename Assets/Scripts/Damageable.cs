using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public Action<float> OnRecieveDamage = delegate { };
    public Action<float> OnResiveBossDamage = delegate { };

    public void DoDamage(float damage)
    {
        OnRecieveDamage(damage);
    }

    public void DoBossDamage(float damageBoss)
    {
        OnResiveBossDamage(damageBoss);
    }
}
