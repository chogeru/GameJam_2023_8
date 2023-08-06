using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        //Debug.Log("‰Á‘¬");
    }

    void Update()
    {
        
    }
}
