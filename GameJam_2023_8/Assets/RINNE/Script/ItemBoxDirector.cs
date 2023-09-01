using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxDirector : MonoBehaviour
{
    [SerializeField]
    GameObject itembox;
    public int count = 0;

    private void Update()
    {
        if(itembox.activeSelf == false)
        {
            count++;
            if(count == 1)
            StartCoroutine("Resporn");
        }
    }

    IEnumerator Resporn()
    {
        yield return new WaitForSeconds(2.0f);
        itembox.SetActive(true);
        count = 0;
    }
}
