using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxDirector : MonoBehaviour
{
    [SerializeField]
    GameObject itembox;

    private void Update()
    {
        if(itembox.activeSelf == false)
        {
            StartCoroutine("Resporn");
        }
    }

    IEnumerator Resporn()
    {
        yield return new WaitForSeconds(2.0f);
        itembox.SetActive(true);
    }
}
