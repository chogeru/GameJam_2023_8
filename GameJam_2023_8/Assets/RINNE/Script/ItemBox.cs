using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        
        if(collision.CompareTag("Car"))
        {
            gameObject.SetActive(false);
        }
    }
}
