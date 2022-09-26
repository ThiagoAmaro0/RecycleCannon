using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Trash;

public class TrashSpawnManager : MonoBehaviour
{
    [SerializeField] private Trash _metalPrefab;
    [SerializeField] private Trash _plasticPrefab;
    [SerializeField] private Trash _organicPrefab;
    [SerializeField] private List<Trash> _pullingList;

    public static TrashSpawnManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void SpawnTrash(TrashType type, Vector3 position)
    {
        Trash prefab;
        switch (type)
        {
            case TrashType.Metal:
                prefab = _metalPrefab;
                break;
            case TrashType.Plastic:
                prefab = _plasticPrefab;
                break;
            case TrashType.Organic:
                prefab = _organicPrefab;
                break;
            default:
                prefab = _metalPrefab;
                break;
        }
        foreach (Trash trash in _pullingList)
        {
            if (!trash.gameObject.activeSelf)
            {
                if (trash.GetTrashType() == type)
                {
                    Spawn(trash.gameObject, false, position);
                    return;
                }
            }
        }
        Spawn(prefab.gameObject, true, position);

    }

    public void Spawn(GameObject prefab, bool isNew, Vector3 position)
    {
        if (isNew)
        {
            _pullingList.Add(Instantiate(prefab, position, Quaternion.identity, transform).GetComponent<Trash>());
        }
        else
        {
            prefab.SetActive(true);
            prefab.transform.position = position;
        }
    }
}