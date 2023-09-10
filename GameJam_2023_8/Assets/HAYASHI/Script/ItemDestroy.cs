using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")||collision.gameObject.CompareTag("Car"))
        {
            Destroy(gameObject);
        }
            
    }
}
