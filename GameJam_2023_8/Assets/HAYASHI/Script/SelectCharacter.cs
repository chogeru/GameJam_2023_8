using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Windows;

public class SelectCharacter : MonoBehaviour
{
    [SerializeField,Header("�^�C�g�����")]
    private GameObject m_TitleUI;
    [SerializeField,Header("�L�����N�^�[�I�����")]
    private GameObject m_SelectCharacterUI;
    [SerializeField,Header("�L�����N�^�[�m�F�pUI")]
    private GameObject m_CheckUI;
    private void Start()
    {
        m_TitleUI.SetActive(true);
        m_CheckUI.SetActive(false);
        m_SelectCharacterUI.SetActive(false);
        
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            m_TitleUI.SetActive(false);
            m_SelectCharacterUI.SetActive(true);
        }
    }
   
    public void OnClickToCheckUI()
    {
        m_CheckUI.SetActive(true);
    }
    public void OnClickToCheckUIClose()
    {
        m_CheckUI.SetActive(false);
    }

}
