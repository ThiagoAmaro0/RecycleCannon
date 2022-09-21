using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    [SerializeField] private Trash.TrashType trashType;
    private float _collectTime;
    private PlayerInventory _inventory;

    private void Update()
    {
        if (_inventory)
        {
            Collect();
        }
    }

    private void Collect()
    {
        if (_inventory.GetInventory().Count == 0)
            return;
        if (_inventory.GetInventory()[0].GetTrashType() == trashType)
        {
            if (_collectTime < Time.time)
            {
                _collectTime = Time.time + 0.5f;
                _inventory.Remove(0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerInventory>(out PlayerInventory inventory))
        {
            _inventory = inventory;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerInventory>(out PlayerInventory inventory))
        {
            _inventory = null;
        }
    }
}
