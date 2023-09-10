using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMove : MonoBehaviour
{
    private float m_HumanMoveSpeed=5;
    private float m_HumanMoveDistance=10;

    private float m_MoveCoolTime=2;
    private float m_NextMoveTime;

    private float m_MypositionX;
    private float m_MypositionY;
    private float m_TargetPosY;
    private float m_TargetPosX;
    private void Start()
    {
        m_MypositionX=transform.position.x;
        m_MypositionY=transform.position.z;
    }
    void Update()
    {

        if (Time.time >= m_NextMoveTime)
        {
            NextMoveCoolTime();
        }
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(m_TargetPosX, transform.position.y,m_TargetPosY), m_HumanMoveSpeed * Time.deltaTime);
       
    }
    private void NextMoveCoolTime()
    {
        m_TargetPosX = m_MypositionX + Random.Range(-m_HumanMoveDistance, m_HumanMoveDistance);
        m_TargetPosY = m_MypositionY + Random.Range(-m_HumanMoveDistance, m_HumanMoveDistance);
        m_NextMoveTime = Time.time + m_MoveCoolTime;
    }
}
