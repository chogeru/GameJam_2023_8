using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxHaya : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        
        if(collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
        if (collision.CompareTag("Car"))
        {
            gameObject.SetActive(false);
        }
    }
}
