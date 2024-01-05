using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private bool _isOccupied = false;
    public bool IsOccupied => _isOccupied;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void CheckOccupiedSpawner()
    {
        _isOccupied = true;
    }
}
