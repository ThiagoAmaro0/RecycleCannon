using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private Trash.TrashType _type;
    private Vector3 _startPos;
    private void Start()
    {
        _startPos = transform.position;
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemy))
        {
            enemy.Hit(1, _type);
        }

        transform.position = _startPos;
        gameObject.SetActive(false);
    }
}
