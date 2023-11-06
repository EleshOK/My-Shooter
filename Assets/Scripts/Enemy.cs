using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _healt;
    [SerializeField] private int _speed;
    private Rigidbody2D _rigidbody;
    private Player _target;
    [SerializeField] private GameObject _spawn;
    void Start()
    {
        _target = FindObjectOfType<Player>();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(gameObject.transform.position, _target.transform.position, _speed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _target.transform.position - transform.position);
    }
}
