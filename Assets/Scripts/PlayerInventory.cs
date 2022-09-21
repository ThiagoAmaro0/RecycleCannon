using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform _backpackTransform;
    private List<Trash> _invetory;
    // Start is called before the first frame update
    void Start()
    {
        _invetory = new List<Trash>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Trash>(out Trash trash))
        {
            Grab(trash);
        }
    }

    private void Grab(Trash trash)
    {
        if (_invetory.Contains(trash))
            return;

        trash.transform.parent = _backpackTransform;
        trash.transform.localPosition = new Vector3(0, _invetory.Count * 0.35f, 0);
        trash.transform.localRotation = Quaternion.identity;
        _invetory.Add(trash);
    }
}
