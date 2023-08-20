using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject item;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine("Resporn");
        }
    }

    IEnumerator Resporn()
    {
        yield return new WaitForSeconds(2.0f);
        Instantiate(item);
    }

    void Start()
    {
        
    }

    void Update()
    {

    }
}
