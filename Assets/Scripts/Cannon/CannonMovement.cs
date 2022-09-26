using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonMovement : MonoBehaviour
{
    [SerializeField] private float _aimSpeed;
    [SerializeField] private float _angleMultiplier;
    [SerializeField] private GameObject _aim;
    private Vector3 _velocity;

    private bool _stop;

    // Update is called once per frame
    void Update()
    {
        if (_stop)
            return;
        _aim.transform.position += _velocity * _aimSpeed * Time.deltaTime;
        Rotate();
    }

    private void Rotate()
    {
        transform.LookAt(_aim.transform);
        Vector3 euler = transform.eulerAngles;
        euler = new Vector3(-Vector3.Distance(transform.position, _aim.transform.position) * _angleMultiplier, euler.y, euler.z);
        transform.root.eulerAngles = euler;
    }

    public void OnAim(InputValue value)
    {
        if (_stop)
            return;
        Vector2 input = value.Get<Vector2>();

        _aim.SetActive(input != Vector2.zero);
        _velocity = new Vector3(input.x, 0, input.y);
    }

    public void Stop()
    {
        _stop = true;
    }
}
