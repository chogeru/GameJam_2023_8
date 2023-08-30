using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapHit : MonoBehaviour
{
    [SerializeField]
    // 停止用タイマースクリプト
    private LapTime m_TimerScript;
    [SerializeField]
    private LapTime m_TimerScript2;
    [SerializeField]
    private LapTime m_TimerScript3;
    [SerializeField]
    //周回時にアクティブにするオブジェクト
    private GameObject m_ActiveTime;
    [SerializeField]
    private GameObject m_ActiveTime2;
    private int m_Lap;
    [SerializeField]
    private GameObject m_StartLapUI;
    private float m_StartUITime=3;
    private float m_Time;
    //開始三秒後に計測開始
    private void Start()
    {
       m_StartLapUI.SetActive(false);
    }
    private void Update()
    {
        m_Time += Time.deltaTime;
        if (m_Time > m_StartUITime)
        {
            m_StartLapUI.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            m_Lap++;
            // タイマーを停止
            m_TimerScript.StopTimer();
            if (m_Lap == 1)
            {
                m_ActiveTime.SetActive(true);
            }
            if(m_Lap == 2)
            {
                m_ActiveTime2.SetActive(true);
                m_TimerScript2.StopTimer();
            }
            if (m_Lap == 3)
            {
                m_TimerScript3.StopTimer();
            }
        }
    }
}
