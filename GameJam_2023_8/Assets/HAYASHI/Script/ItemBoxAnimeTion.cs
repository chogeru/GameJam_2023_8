using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemBoxAnimeTion : MonoBehaviour
{
    [Header("�A�C�e���Q�b�g���̃A�j���[�V�����p�I�u�W�F�N�g")]
    private GameObject m_ItemGetAnimeObj;
    [SerializeField,Header("UI�̊i�[�p")]
    private GameObject[] m_UIObjects; 
    [SerializeField,Header("�^�O")]
    private string m_UiTag = "ItemUI"; 
    [SerializeField]
    private bool isItemGet;
    // UI���A�N�e�B�u�ɂ��鋖�t���O
    private bool isCanActivateUI = false;
    // UI���A�N�e�B�u�ɂ���܂ł̑ҋ@����
    private float m_ActivationDelay = 3f;
    // �ҋ@�^�C�}�[
    private float m_ActivationTimer = 0f; 
    [SerializeField, Header("�A�C�e����������SE")]
    private GameObject m_ItemGetSE;
    [SerializeField, Header("�A�C�e���m�莞��SE")]
    private GameObject m_ItemSetSE;
    // Start is called before the first frame update
    void Start()
    {
        m_ItemGetAnimeObj = GameObject.Find("�A�C�e���A�j���[�V����");
        m_ItemGetAnimeObj.SetActive(false);
        m_ItemGetSE = GameObject.Find("ItemGetSE");
        m_ItemGetSE.SetActive(false);
        m_ItemSetSE = GameObject.Find("ItemSetSE");
        m_ItemSetSE.SetActive(false);
        m_UIObjects = GameObject.FindGameObjectsWithTag(m_UiTag);
      
        // ������Ԃł͑S�Ă�UI�I�u�W�F�N�g���A�N�e�B�u�ɂ���
        foreach (GameObject uiObject in m_UIObjects)
        {
            uiObject.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            m_ItemGetAnimeObj.SetActive(true);
            //UI�A�N�e�B�u
            isCanActivateUI = true; 
            //�A�C�e���Q�b�g����SE���Đ�
            m_ItemGetSE.SetActive(true);
            // �ҋ@�^�C�}�[��������
            m_ActivationTimer = 0f;
        }
    }

    private void Update()
    {
        // UI���A�N�e�B�u�ȏꍇ
        if (isCanActivateUI)
        {
            // �ҋ@�^�C�}�[�𑝉�������
            m_ActivationTimer += Time.deltaTime;

            // �ҋ@�^�C�}�[���w��̎��Ԃ𒴂����ꍇ�A�����_����UI�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            if (m_ActivationTimer >= m_ActivationDelay)
            {
                int randomIndex = Random.Range(0, m_UIObjects.Length);
                for (int i = 0; i < m_UIObjects.Length; i++)
                {
                    m_UIObjects[i].SetActive(i == randomIndex);
                }
                //�A�C�e���Z�b�g����SE���Đ�
                m_ItemSetSE.SetActive(true);
                //���łɍĐ�����Ă���A�C�e���Q�b�g����SE���X�g�b�v
                m_ItemGetSE.SetActive(false);
                // UI�̃A�N�e�B�u�𖳌������A�ҋ@�^�C�}�[�����Z�b�g
                isCanActivateUI = false;
                m_ActivationTimer = 0f;
            }
        }
    }
}
