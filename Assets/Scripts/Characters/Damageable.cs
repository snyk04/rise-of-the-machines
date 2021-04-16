﻿using Classes;
using Classes.TryInHierarchie;
using UnityEngine;

public class Damageable : MonoBehaviour {
    [SerializeField] private ParticleSystem Blood_1;
    [SerializeField] private ParticleSystem Blood_2;
    private float maxHealth;

    private IDamageable damageble;

    public void Initialize(float maxHealth, float moveSpeed, float armor, Transform transform,
        Animator animator = null) {
        this.maxHealth = maxHealth;
        damageble = new Enemy(maxHealth, armor, moveSpeed, transform, animator);
    }

    public void TakeDamage(float damage) {
        damageble = damageble ?? new Breakable(maxHealth, transform);
        damageble.Health.TakeDamage(damage);
        if (damageble is Enemy) {
            Blood_1.Emit(1);
            Blood_2.Emit(1);
        }
    }
}