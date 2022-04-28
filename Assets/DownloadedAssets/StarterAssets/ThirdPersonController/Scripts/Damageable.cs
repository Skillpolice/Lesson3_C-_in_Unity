using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public Action<float> OnRecieveDamage = delegate { };

    public void DoDamage(float damage)
    {
        OnRecieveDamage(damage);
    }
}
