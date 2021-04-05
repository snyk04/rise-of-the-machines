﻿using Classes;
using Classes.TryInHierarchie;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    private IDamageable damageble;

    public void Initialize(float maxHealth, float moveSpeed, float armor, Transform transform, Animator animator = null)
    {
        damageble = new Enemy(maxHealth, armor, moveSpeed, transform, animator);
    }

    public void TakeDamage(float damage)
    {
        damageble = damageble ?? new Breakable(maxHealth, transform);
        damageble.Health.TakeDamage(damage);
    }
}
