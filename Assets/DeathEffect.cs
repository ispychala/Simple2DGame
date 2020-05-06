using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public float time = 5f;

    void Start()
    {
        Destroy(gameObject, time);
    }
    
}
