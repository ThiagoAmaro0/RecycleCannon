using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour, IHitable
{
    [SerializeField] private int _health;
    private bool _dead;
    [SerializeField] public UnityEvent dieAction;
    public void Hit(int damage)
    {
        _health -= damage;
        if (_health <= 0 && !_dead)
        {
            _dead = true;
            _health = 0;
            dieAction?.Invoke();
            gameObject.layer = LayerMask.NameToLayer("Dead");
        }
    }
}
