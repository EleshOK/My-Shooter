using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private int _damage;
    private Rigidbody2D _rigidBody;
    void Start()
    {
        
    }

    public void SetDirection(Vector2 direction)
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.velocity = direction.normalized * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            Destroy(gameObject);
        }
    }

}
