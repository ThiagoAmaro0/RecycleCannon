
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform _backpackTransform;
    private List<Trash> _inventory;
    // Start is called before the first frame update
    void Start()
    {
        _inventory = new List<Trash>();
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
        if (_inventory.Contains(trash))
            return;

        trash.transform.parent = _backpackTransform;
        trash.transform.localPosition = new Vector3(0, _inventory.Count * 0.35f, 0);
        trash.transform.localRotation = Quaternion.identity;
        _inventory.Add(trash);
        TrashSpawnManager.instance.SpawnTrash(trash.GetTrashType(), new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-3.5f, 3.5f)));
    }

    public List<Trash> GetInventory()
    {
        return _inventory;
    }

    public void Remove(int index)
    {
        _inventory[0].transform.parent = TrashSpawnManager.instance.transform;
        _inventory[0].gameObject.SetActive(false);
        _inventory.RemoveAt(0);
        foreach (Trash trash in _inventory)
        {
            trash.transform.position -= new Vector3(0, 0.35f, 0);
        }
    }
}
