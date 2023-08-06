using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cal : MonoBehaviour
{
    public int minCal;
    public int maxCal;

    private void Start()
    {
        ChangeCal();
    }

    public  void ChangeCal()
    {
        //ƒ‰ƒ“ƒ_ƒ€ƒJƒƒŠ[¶¬
        int cal = Random.Range(minCal, maxCal);
    }
}
