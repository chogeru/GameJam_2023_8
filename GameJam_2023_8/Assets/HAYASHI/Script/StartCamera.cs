
using HAYASHI.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject m_PlayerCamera;
    [SerializeField]
    private GameObject m_StartAnimeCamera;
    [SerializeField]
    private GameObject m_StartText;
    [SerializeField]
    private GameObject m_CountDownTimer;
    private float m_Timer;
    private float m_StartCameraDestroy = 4f;
    
    // Start is called before the first frame update
    void Start()
    {
        m_PlayerCamera.SetActive(false);
        m_StartText.SetActive(false);
        m_CountDownTimer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;
        if(m_StartCameraDestroy<m_Timer)
        {
            m_CountDownTimer.SetActive(true);
            m_PlayerCamera.SetActive(true);
            m_StartText.SetActive(true);
            Destroy(m_StartAnimeCamera);
        }
    }
}
