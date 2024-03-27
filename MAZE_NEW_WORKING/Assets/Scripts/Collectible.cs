using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static event Action OnCollected;
    // Update is called once per frame
    void Update()
    {
        //transform.localRotation = Quaternion.Euler(0f, Time.time * 180f, 0);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
