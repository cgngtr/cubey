using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public Transform GetDestination()
    { 
        return destination; 
    }
}
