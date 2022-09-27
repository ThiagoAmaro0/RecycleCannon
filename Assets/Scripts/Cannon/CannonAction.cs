using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CannonAction : MonoBehaviour
{
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private Rigidbody _metalAmmo, _organicAmmo, _plasticAmmo;
    [SerializeField] private GameObject _metalPreview, _organicPreview, _plasticPreview;
    [SerializeField] private Transform _startPos, _aim;
    private Trash.TrashType _equippedAmmo;
    private Dictionary<Trash.TrashType, int> _ammo;
    private Dictionary<Trash.TrashType, Rigidbody> _rigidbodys;
    private Dictionary<Trash.TrashType, GameObject> _previews;
    private bool _loaded;
    private bool _stop;

    public static CannonAction instance;

    private void Awake()
    {
        _ammo = new Dictionary<Trash.TrashType, int>();
        _ammo.Add(Trash.TrashType.Metal, 0);
        _ammo.Add(Trash.TrashType.Organic, 0);
        _ammo.Add(Trash.TrashType.Plastic, 0);
        _rigidbodys = new Dictionary<Trash.TrashType, Rigidbody>();
        _rigidbodys.Add(Trash.TrashType.Metal, _metalAmmo);
        _rigidbodys.Add(Trash.TrashType.Organic, _organicAmmo);
        _rigidbodys.Add(Trash.TrashType.Plastic, _plasticAmmo);
        _previews = new Dictionary<Trash.TrashType, GameObject>();
        _previews.Add(Trash.TrashType.Metal, _metalPreview);
        _previews.Add(Trash.TrashType.Organic, _organicPreview);
        _previews.Add(Trash.TrashType.Plastic, _plasticPreview);
        instance = this;
    }

    public void OnFire()
    {
        if (!_loaded || _stop)
            return;
        if (_rigidbodys[_equippedAmmo].gameObject.activeSelf)
            return;
        if (_ammo[_equippedAmmo] == 0)
            return;

        _ammo[_equippedAmmo]--;
        Fire(_rigidbodys[_equippedAmmo]);


        if (_ammo[_equippedAmmo] == 0)
            _previews[_equippedAmmo].SetActive(false);



    }

    private void Fire(Rigidbody rb)
    {
        float distance = Vector3.Distance(_startPos.position, _aim.position);
        Vector3 middle = (_startPos.position + _aim.position) / 2;
        rb.gameObject.SetActive(true);
        rb.MovePosition(_startPos.position);
        rb.transform.parent = null;
        rb.DOPath(
            new Vector3[] { _startPos.position, new Vector3(middle.x, distance / 2, middle.z), _aim.position },
            distance / _projectileSpeed, PathType.CatmullRom, PathMode.Ignore).SetEase(Ease.Linear);
    }

    public void EquipAmmo(Trash.TrashType type)
    {
        _loaded = true;
        _equippedAmmo = type;
        if (_ammo[type] == 0)
            return;
        _previews[_equippedAmmo].SetActive(true);
    }

    public void AddAmmo(Trash.TrashType type)
    {
        _ammo[type] += 1;
    }

    public void Stop()
    {
        _stop = true;
    }
}