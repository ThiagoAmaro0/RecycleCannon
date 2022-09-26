using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBehaviour : Hitable
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _perception;
    [SerializeField] private float _trashSpawnDelay;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _base;
    [SerializeField] private Trash.TrashType _trashType;
    [SerializeField] private Trash.TrashType[] _weakness;
    [SerializeField] private Animator _anim;

    private float _spawnTime;
    private bool _attacking;
    private Rigidbody _rb;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _player = GameObject.Find("Player").transform;
        _base = GameObject.Find("Wall").transform;
        _spawnTime = Time.time + _trashSpawnDelay;
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
            _anim.SetBool("Walk", true);
            if (_spawnTime < Time.time)
            {
                _spawnTime = Time.time + _trashSpawnDelay;
                TrashSpawnManager.instance.SpawnTrash(_trashType, transform.position + new Vector3(0, 0, 0));
            }
            Vector3 dir = target.position - transform.position;
            _rb.velocity = new Vector3(dir.x, 0, dir.z).normalized * _speed;
            transform.forward = _rb.velocity.normalized;
        }
        else if (!_attacking)
        {
            _anim.SetBool("Walk", false);
            StartCoroutine(Attack(target));
        }
    }

    private IEnumerator Attack(Transform target)
    {
        _attacking = true;
        _anim.SetTrigger("Attack");
        yield return new WaitForSeconds(_attackSpeed);
        if (Vector3.Distance(transform.position, target.position) < _attackRange)
        {
            if (target.TryGetComponent<Hitable>(out Hitable obj))
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

    protected override void Die()
    {
        base.Die();
        TrashSpawnManager.instance.SpawnTrash(_trashType, transform.position + new Vector3(0.1f, 0, -0.1f));
        TrashSpawnManager.instance.SpawnTrash(_trashType, transform.position + new Vector3(-0.1f, 0, -0.1f));
        TrashSpawnManager.instance.SpawnTrash(_trashType, transform.position + new Vector3(0, 0, 0.1f));
        _rb.velocity = Vector3.zero;
        gameObject.layer = LayerMask.NameToLayer("Dead");
        Destroy(gameObject, 2);
    }
}
