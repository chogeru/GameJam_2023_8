using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cal : MonoBehaviour
{
    public int minCal;
    public int maxCal;
    public int rand;

    private void Start()
    {
        ChangeCal();
    }

    public int ChangeCal()
    {
        //ƒ‰ƒ“ƒ_ƒ€ƒJƒƒŠ[¶¬
        rand = Random.Range(minCal, maxCal);
        return rand;
    }
}
