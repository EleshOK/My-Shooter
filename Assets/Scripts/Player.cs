
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _playerSprite;
    [SerializeField] private AudioSource _runAudioSourse;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private GameObject _bulletPoint;
    [SerializeField] private int _damageTimer;
    [SerializeField] private int _healt;
    [SerializeField] Blood _blood;
    private float _elapsedTime;
    private bool _isShot;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _gamePoint;
    [SerializeField] private Transform _finishPoint;
    private GameManager _gameManager;
    [SerializeField] private float _timePlayerMove;
    [SerializeField] Collider2D _collider;
    [SerializeField] private AudioSource _shootAudioSourse;
    [SerializeField] private AudioSource _mainAudioSourse;
    [SerializeField] private TMP_Text _healtText;
    [SerializeField] private Weapon _weapon;



    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _healt = 3;
         _isShot = true;
        _healtText.text = "3";
    }

    void Update()
    {
        if (_gameManager.state == GameManager.State.Game)
        {
            Move();
            _collider.enabled = true;
        }

        if (_gameManager.state == GameManager.State.NextLvl)
        {
            transform.position = _startPoint.transform.position;
        }

        IsShoot();
        Shot();
        if (_gameManager.state == GameManager.State.Start)
        {
            StartGame();
            _animator.SetBool("IsWalk", true);
        }

        if (_gameManager.state == GameManager.State.Finish)
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, _finishPoint.transform.position, _timePlayerMove * Time.deltaTime);
            _playerSprite.transform.rotation = Quaternion.LookRotation(Vector3.forward, _finishPoint.transform.position);
            _animator.SetBool("IsWalk", true);
            _collider.enabled = false;
            if (Vector3.Distance(gameObject.transform.position, _finishPoint.transform.position) <= 0.3)
            {
                gameObject.transform.position = _startPoint.transform.position;

            }
        }
        
        
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

    private void IsShoot()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= _damageTimer)
        {
            _isShot = true;
            _elapsedTime = 0;
        }
    }

    private void Shot()
    {
        if(Input.GetMouseButtonDown(0) && _isShot == true && _weapon.CanShoot) 
        {
            Bullet bullet = Instantiate(_bullet, _bulletPoint.transform.position, Quaternion.identity);
            bullet.transform.Rotate(_bulletPoint.transform.eulerAngles, Space.World);
            bullet.SetDirection(_bulletPoint.transform.right);
            _isShot = false;
            _shootAudioSourse.Play();
            _weapon.Shoot();

        }
    }

    public void TakeDamage(int damage)
    {
        _healt -= damage;
        Debug.Log(_healt);
        _healtText.text = _healt.ToString();
        Instantiate(_blood, gameObject.transform.position, transform.rotation);
        if (_healt <= 0 ) {
            _gameManager.SetLose();
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        transform.position = Vector3.MoveTowards(transform.position, _gamePoint.position, _timePlayerMove * Time.deltaTime);
        if (Vector3.Distance(gameObject.transform.position, _gamePoint.transform.position) < 0.3)
        {
            _gameManager.SetGame();
        }
    }
}
