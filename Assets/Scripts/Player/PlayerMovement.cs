using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Animator _anim;
    private Rigidbody _rb;
    private Vector3 _velocity;

    private bool _stop;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (_stop)
            return;
        _rb.velocity = _velocity * _speed;
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        _velocity = new Vector3(input.x, 0, input.y);
        if (input != Vector2.zero)
        {
            _anim.SetBool("Walk", true);
            transform.forward = _velocity;
        }
        else
        {
            _anim.SetBool("Walk", false);
        }
    }

    public void Stop()
    {
        _stop = true;
        _velocity = new Vector3();
    }
}
