using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RankChecker : MonoBehaviour
{
    [SerializeField]
    private GameObject[] CarList;
    // Start is called before the first frame update

    [SerializeField]
    private GameObject[] CheckPoint;

    [SerializeField]
    private float[] CarRankCalc;

    [SerializeField]
    private int CheckPointNo = 0;

    void Start()
    {
        CarList = GameObject.FindGameObjectsWithTag("Car");
        CarRankCalc = new float[CarList.Length];
    }

    // Update is called once per frame
    void Update()
    {
        //チェックポイント確認
        for(int i = 0; i< CheckPoint.Length; i++)
        {
            if (CheckPoint[i].activeSelf)
            {
                CheckPointNo = i;
                break;
            }
        }

        for(int i=0;i<CarList.Length;i++)
        {
            Vector3 point = CheckPoint[CheckPointNo].transform.position;
            //CarRankCalc[i] = (CarList[i].transform.position.z -  point.z);
            CarRankCalc[i] = Vector3.Distance(CarList[i].transform.position,point);
        }

        //順位並び替え
        for(int i=0;i< CarRankCalc.Length;i++)
        {
            for(int j = i+1; j < CarRankCalc.Length;j++)
            {
                if (CarRankCalc[i] > CarRankCalc[j])
                {
                    float tmp = CarRankCalc[i];
                    CarRankCalc[i] = CarRankCalc[j];
                    CarRankCalc[j] = tmp;

                    GameObject obj = CarList[i];
                    CarList[i] = CarList[j];
                    CarList[j] = obj;
                }
            }
        }
    }

    public string GetRankText()
    {
        string res = "";

        for (int i = 0; i < CarList.Length; i++)
        {
            res +=CarList[i].name.ToString();
        }

        return res;
    }
}
