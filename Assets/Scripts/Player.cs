using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _playerSprite;
    [SerializeField] private AudioSource _runAudioSourse;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private GameObject _bulletPoint;


    void Start()
    {
         
    }

    void Update()
    {
        Move();
        Shot();
    }

    private void Move()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 direction = Camera.main.ScreenToWorldPoint(mousePosition) - _playerSprite.transform.position;
        _playerSprite.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if(_runAudioSourse.isPlaying == false)
            {
                _runAudioSourse.Play();
            }
            _animator.SetBool("IsWalk", true);
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(0, _speed * Time.deltaTime, 0);
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(0, -_speed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-_speed * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(_speed * Time.deltaTime, 0, 0);
            }
        }
        else
        {
            _animator.SetBool("IsWalk", false);
            _runAudioSourse.Stop();
        }


        
    }

    private void Shot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Bullet bullet = Instantiate(_bullet, _bulletPoint.transform.position, Quaternion.identity);
            bullet.transform.Rotate(_bulletPoint.transform.eulerAngles, Space.World);
            bullet.SetDirection(_bulletPoint.transform.right);
        }
    }
}
