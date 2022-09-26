using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] private TrashType _type;

    public TrashType GetTrashType()
    {
        return _type;
    }

    public enum TrashType
    {
        Metal, Plastic, Organic
    }
}
