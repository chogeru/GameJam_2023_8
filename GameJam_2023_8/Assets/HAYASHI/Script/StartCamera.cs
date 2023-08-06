
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject m_PlayerCamera;
    [SerializeField]
    private GameObject m_StartAnimeCamera;
    // Start is called before the first frame update
    void Start()
    {
        m_PlayerCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
