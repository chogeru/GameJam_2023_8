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
        //ランダムカロリー生成
        int cal = Random.Range(minCal, maxCal);
    }
}
