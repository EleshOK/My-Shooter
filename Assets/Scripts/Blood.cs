using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    [SerializeField] private float _time;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, Mathf.MoveTowards(_spriteRenderer.color.a, 0, _time * Time.deltaTime));
        if (_spriteRenderer.color.a <= 0 )
        {
            Destroy(gameObject);
        }

    }

}
