using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePickedUp : MonoBehaviour
{
    private AudioSource eat;
    // Start is called before the first frame update
    void Start()
    {
        eat = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            if (!eat.isPlaying)
            {
                eat.Play();
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
