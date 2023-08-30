using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapHit : MonoBehaviour
{
    [SerializeField]
    // ��~�p�^�C�}�[�X�N���v�g
    private LapTime m_TimerScript;
    [SerializeField]
    private LapTime m_TimerScript2;
    [SerializeField]
    private LapTime m_TimerScript3;
    [SerializeField]
    //���񎞂ɃA�N�e�B�u�ɂ���I�u�W�F�N�g
    private GameObject m_ActiveTime;
    [SerializeField]
    private GameObject m_ActiveTime2;
    private int m_Lap;
    [SerializeField]
    private GameObject m_StartLapUI;
    private float m_StartUITime=3;
    private float m_Time;
    //�J�n�O�b��Ɍv���J�n
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
            // �^�C�}�[���~
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
