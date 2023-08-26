using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    private float m_DestroyTime = 2;
    private float m_Time = 0;
    private void Update()
    {
        m_Time += Time.deltaTime;
        if(m_Time > m_DestroyTime)
        {
            Destroy(gameObject);
        }
    }
}
