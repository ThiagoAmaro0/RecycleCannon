using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _metalPrefab;
    [SerializeField] private GameObject _plasticPrefab;
    [SerializeField] private GameObject _organicPrefab;
    private List<GameObject> _pullingList;
    // Start is called before the first frame update
    void Start()
    {
        _pullingList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
