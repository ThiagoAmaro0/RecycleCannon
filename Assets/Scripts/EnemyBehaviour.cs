using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _perception;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _base;
    [SerializeField] private Trash.TrashType[] _weakness;

    private bool _attacking;
    private bool _dead;
    private Rigidbody _rb;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _player = FindObjectOfType<PlayerCombat>().transform;
        _base = FindObjectOfType<Wall>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_dead)
            return;
        if (Vector3.Distance(transform.position, _player.position) < _perception)
        {
            Move(_player);
        }
        else
        {
            Move(_base);
        }
    }

    private void Move(Transform target)
    {
        if (Vector3.Distance(transform.position, target.position) > _attackRange)
        {
            Vector3 dir = target.position - transform.position;
            _rb.velocity = new Vector3(dir.x, 0, dir.z).normalized * _speed;
        }
        else if (!_attacking)
        {
            StartCoroutine(Attack(target));
        }
    }

    private IEnumerator Attack(Transform target)
    {
        _attacking = true;
        yield return new WaitForSeconds(_attackSpeed);
        if (Vector3.Distance(transform.position, target.position) < _attackRange)
        {
            if (target.TryGetComponent<IHitable>(out IHitable obj))
                obj.Hit(_damage);
        }
        _attacking = false;
    }

    public void Hit(int damage, Trash.TrashType type)
    {
        if (!_weakness.Contains(type))
            return;
        _health -= damage;
        if (_health <= 0 && !_dead)
            Die();
    }

    private void Die()
    {
        _dead = true;
        _rb.velocity = Vector3.zero;
        gameObject.layer = LayerMask.NameToLayer("Dead");
        Destroy(gameObject, 2);
    }
}
