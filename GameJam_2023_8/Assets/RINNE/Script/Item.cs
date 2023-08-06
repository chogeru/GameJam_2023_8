using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject item;

    private void Awake()
    {
        item = GameObject.Find("Item");
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(item != null)
        {
            
        }
        else
        {
            item = new GameObject();
        }
    }
}
