using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hitable : MonoBehaviour
{
    [SerializeField] protected int _health;
    private int _currentHealth;
    [SerializeField] public UnityEvent dieAction;
    protected bool _dead;
    private void Awake()
    {
        _currentHealth = _health;
    }
    public virtual void Hit(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0 && !_dead)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        _dead = true;
        dieAction?.Invoke();
    }

    public int GetHealth()
    {
        return _currentHealth;
    }
    public float GetPercentHealth()
    {
        return _currentHealth / (float)_health;
    }
}
