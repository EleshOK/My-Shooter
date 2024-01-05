using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int _maxCount;
    [SerializeField] private int _count;
    [SerializeField] private float _reloadTime;
    [SerializeField] private bool _canShoot;
    [SerializeField] private float _elapsedTime;
    [SerializeField] private TMP_Text _weaponText;
    public bool CanShoot => _canShoot;
    void Start()
    {
        _weaponText.text = $"{_count}/{_maxCount}";
    }

    void Update()
    {
        Reload();

    }

    public void Shoot()
    {
        if (_count > 1)
        {
            _count--;
            _weaponText.text = $"{_count}/{_maxCount}";
        }

        else
        {
            _count = 0;
            _canShoot = false;
            _weaponText.text = $"{_count}/{_maxCount}";
        }
    }

    public void Reload()
    {
        if (_canShoot == true)
        {
            return;
        }

        else
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= _reloadTime)
            {
                _count = _maxCount;
                _elapsedTime = 0;
                _canShoot = true;
                _weaponText.text = $"{_count}/{_maxCount}";
            }
        }
    }
}
