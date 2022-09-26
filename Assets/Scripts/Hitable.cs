using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hitable : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] public UnityEvent dieAction;
    protected bool _dead;
    public virtual void Hit(int damage)
    {
        _health -= damage;
        if (_health <= 0 && !_dead)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        _dead = true;
        dieAction?.Invoke();
    }
}
