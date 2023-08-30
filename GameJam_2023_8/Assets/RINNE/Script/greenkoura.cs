using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greenkoura : MonoBehaviour
{
    public float move_speed;
    float direction;
    GameObject player;
    void Start()
    {
        this.player = GameObject.Find("Player (1)");
        direction = this.player.transform.localEulerAngles.y;
        transform.Rotate(0,direction,0);
    }

    void Update()
    { 
        transform.Translate(0, 0, move_speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Destroy(gameObject);
        }
    }
}
