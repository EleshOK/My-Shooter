using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _healt;
    [SerializeField] private float _speed;
    private Rigidbody2D _rigidbody;
    private Player _target;
    private float _elapsedTime;
    [SerializeField] private int _damageTimer;
    private bool _isShot;
    [SerializeField] Blood _blood;
    private Animator _animator;

    void Start()
    {
        _healt = _healt * GameManager.Lvl * 0.5f;
        _speed = _speed * GameManager.Lvl * 0.2f;
        _damage = _damage * GameManager.Lvl * 0.5f;
        _target = FindObjectOfType<Player>();
        _animator = GetComponent<Animator>();
        if (_damage < 1)
        {
            _damage = 1;
        }
    }

    void Update()
    {
        if (_target != null)
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position, _target.transform.position, _speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _target.transform.position - transform.position);
            if (Vector3.Distance(_target.transform.position, gameObject.transform.position) <= 1)
            {
                IsShoot();
                Fight();
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet)) {
            Debug.Log("������ ����");
            _healt -= 1;
            Instantiate(_blood, gameObject.transform.position, transform.rotation);
            Debug.Log(_healt);
            if (_healt <= 0)
            {
                GameManager.Score += 1;
                Destroy(gameObject);
            }
        }
    }

    private void IsShoot()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= _damageTimer)
        {
            _isShot = true;
            _elapsedTime = 0;
        }
    }

    private void Fight()
    {
        if (_isShot == true)
        {
            _isShot = false;
            _animator.SetTrigger("IsAttack");
            _target.TakeDamage((int)_damage);
            Debug.Log("����� ������� ����");
        }
    }


}
