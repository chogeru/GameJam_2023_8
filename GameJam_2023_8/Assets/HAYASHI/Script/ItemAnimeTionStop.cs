using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimeTionStop : MonoBehaviour
{
    private float m_AnimteStopTime=3;
    private float m_Time;
    [SerializeField]
    private GameObject m_MyGameObje;
    void Update()
    {
        m_Time += Time.deltaTime;
        if (m_Time > m_AnimteStopTime)
        {
            m_MyGameObje.SetActive(false);
            m_Time = 0;
        }
    }
}
