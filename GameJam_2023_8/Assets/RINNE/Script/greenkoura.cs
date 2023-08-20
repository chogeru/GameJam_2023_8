using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greenkoura : MonoBehaviour
{
    public float move_speed;
    public GameObject green_koura;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(0, 0, move_speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Destroy(green_koura);
        }
    }
}
