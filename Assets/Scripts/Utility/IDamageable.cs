using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damageAmount);

    void HealDamage(int damageAmount);

    Vector3 Position { get; }
}
