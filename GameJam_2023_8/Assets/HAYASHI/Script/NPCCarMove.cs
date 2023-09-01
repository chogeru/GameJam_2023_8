using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
namespace HAYASHI.Script
{

    public class NPCCarMove : MonoBehaviour
    {
        // �ړI�n�̃I�u�W�F�N�g��z��ŕێ�
        [SerializeField]
        private Transform[] m_Destinations;
        // �Ԃ̈ړ����x
        [SerializeField]
        private float m_CarMoveSpeed = 25f;
        // ���݂̖ړI�n�̃C���f�b�N�X
        private int m_CurrentDestinationIndex = 0;

        [SerializeField]
        private float m_StartTimer = 7;
        [SerializeField]
        private float m_Timer;
        private bool isStart = false;

        [SerializeField, Header("�A�C�e���������̃G�t�F�N�g")]
        private GameObject m_ItemEffect;

        [SerializeField, Header("�A�C�e����������SE")]
        private AudioClip m_ItemGetSE;
        private float mVolume = 1;
        [SerializeField, Header("�A�C�e���g�p����SE")]
        private AudioClip m_ItemUseSE;

        [SerializeField]
        private bool isItemSuika = false;
        [SerializeField]
        private bool isItemWater = false;
        [SerializeField]
        private bool isItemSando = false;

        private float m_MinInterval = 2.0f;
        private float m_MaxInterval = 5.0f;

        //���̑��x�ɖ߂��ۂ̃g���K�[
        private bool isOriginalSpeed = false;

        private float m_OriginalSpeedTime = 3;
        private float m_CoolTime;

        //�Ԃ̌��݂̃O���[�h
        private int m_CarGrade = 0;

        //�Ԃ̃I�u�W�F�N�g
        [SerializeField]
        private GameObject m_GreadWara;
        [SerializeField]
        private GameObject m_GreadKI;
        [SerializeField]
        private GameObject m_GreadRenga;

        private float m_PlusCarSpeedKI;

        [SerializeField]
        private Transform m_OriginalObject;
        private float m_Scale = 2f;
        private float m_ScaleUpTime = 10f;

        private Vector3 m_OriginalScale;
        [SerializeField]
        private bool isScaling = false;
        private float m_ScaleCoolTime;

        [SerializeField,Header("���剻�ƃp���[�A�b�v���̃G�t�F�N�g")]
        private GameObject m_ScaleChangeEffect;
        private void Start()
        {
            m_CarMoveSpeed = 0;
            MoveToDestination(m_CurrentDestinationIndex);
            m_OriginalScale = m_OriginalObject.localScale;
        }
        private void Update()
        {
            
            //�ϐ��̒l�͈̔͂��w��
            m_CarGrade = Mathf.Clamp(m_CarGrade, 0, 3);
            if (isStart == false)
            {
                m_Timer += Time.deltaTime;
                if (m_StartTimer < m_Timer)
                {
                    m_CarMoveSpeed = 25;
                    m_Timer = 0;
                    isStart = true;
                }
            }
            if (isItemSando)
            {
                // �����_���Ȏ��Ԑݒ�
                float randomInterval = Random.Range(m_MinInterval, m_MaxInterval);
                //��L�̏����Ō��܂��������_���Ȏ��Ԍo�߂Ŋ֐����Ăяo��
                Invoke("�T���h�C�b�`", randomInterval);
                isItemSando = false;
            }
            if (isItemSuika)
            {
                // �����_���Ȏ��Ԑݒ�
                float randomInterval = Random.Range(m_MinInterval, m_MaxInterval);
                Invoke("�l�p�X�C�J", randomInterval);
                isItemSuika = false;
            }
            if (isItemWater)
            {
                // �����_���Ȏ��Ԑݒ�
                float randomInterval = Random.Range(m_MinInterval, m_MaxInterval);
                Invoke("�f�g�b�N�X�E�H�[�^�[", randomInterval);
                isItemWater = false;
            }
            //���Ԍo�߂Ō��̃X�s�[�h�ɖ߂�����
            if (isOriginalSpeed)
            {
                m_CoolTime += Time.deltaTime;
                if (m_CoolTime > m_OriginalSpeedTime)
                {
                    m_CarMoveSpeed = 25;
                    m_CoolTime = 0;
                    isOriginalSpeed = false;
                }
            }
            if (m_CarGrade == 0)
            {
                m_GreadWara.SetActive(true);
                m_GreadKI.SetActive(false);
                m_GreadRenga.SetActive(false);
                m_PlusCarSpeedKI = 0;
            }


            if (isScaling)
            {
                m_ScaleCoolTime += Time.deltaTime;
                if (m_ScaleCoolTime >= m_ScaleUpTime)
                {
                    m_OriginalObject.localScale = m_OriginalScale;
                    isScaling = false;
                    m_ScaleCoolTime = 0f;
                }
            }
        }
        private void MoveToDestination(int destinationIndex)
        {
            // �ړI�n�������ȃC���f�b�N�X�̏ꍇ�͏I��
            if (destinationIndex >= m_Destinations.Length || destinationIndex < 0)
                return;

            // �ړI�n�ւ̈ړ����J�n
            StartCoroutine(MoveCoroutine(m_Destinations[destinationIndex].position));
        }

        private IEnumerator MoveCoroutine(Vector3 destination)
        {
            //�ԗ����ړI�n�ɓ��B����܂Ń��[�v
            //�ԗ��̌��݈ʒu�ƖړI�n�̋������v�Z��0.05���ȏ�傫���ƃ��[�v����
            while (Vector3.Distance(transform.position, destination) > 0.05f)
            {
                //�ړI�n�̕������v�Z
                Vector3 lookDir = destination - transform.position;
                //�ԗ������������ɉ�]���Ȃ��悤�ɂ���
                lookDir.y = 0f;
                if (lookDir != Vector3.zero)
                {
                    // �ړI�n�̕���������
                    transform.rotation = Quaternion.LookRotation(lookDir);
                }

                // �ړI�n�ւ̈ړ�
                transform.position = Vector3.MoveTowards(transform.position, destination, m_CarMoveSpeed * Time.deltaTime);
                yield return null;
            }

            // �ړI�n�ɓ��������玟�̖ړI�n��
            m_CurrentDestinationIndex++;
            if (m_CurrentDestinationIndex >= m_Destinations.Length)
            {
                // �Ō�̖ړI�n�ɓ��B������ŏ��ɖ߂�
                m_CurrentDestinationIndex = 0;
            }
            // ���̖ړI�n�ֈړ�
            MoveToDestination(m_CurrentDestinationIndex);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Item"))
            {
                // �����_����0����2�̐����𐶐�
                int randomIndex = Random.Range(0, 3);
                //�T�E���h�̍Đ�
                AudioSource.PlayClipAtPoint(m_ItemGetSE,new Vector3(20,20,0), mVolume);
                //�p�[�e�B�N���̕���
                Instantiate(m_ItemEffect.gameObject.transform);
                // �����_���ȃA�C�e���p�֐����Ăяo��
                switch (randomIndex)
                {
                    case 0:
                        //���Ԍo�߂Ŋ֐����Ăяo���p�̃g���K�[
                        isItemSuika = true;
                        break;
                    case 1:
                        isItemWater = true;
                        break;
                    case 2:
                        isItemSando = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void �f�g�b�N�X�E�H�[�^�[()
        {
            m_CarMoveSpeed += 7;
            isOriginalSpeed = true;
            AudioSource.PlayClipAtPoint(m_ItemUseSE, new Vector3(20, 20, 0), mVolume);
        }
        private void �l�p�X�C�J()
        {
            m_CarGrade += 1;
            Vector3 spawnPosition = transform.position - new Vector3(3, 0, 0);
            if (m_CarGrade == 1)
            {
                m_GreadWara.SetActive(false);
                m_GreadKI.SetActive(true);
                m_GreadRenga.SetActive(false);
                m_PlusCarSpeedKI = 3f;
                m_CarMoveSpeed += m_PlusCarSpeedKI;
                Instantiate(m_ScaleChangeEffect, spawnPosition, Quaternion.identity);
            }
            if (m_CarGrade == 2)
            {
                m_GreadWara.SetActive(false);
                m_GreadKI.SetActive(false);
                m_GreadRenga.SetActive(true);
                Instantiate(m_ScaleChangeEffect, spawnPosition, Quaternion.identity);
            }
            AudioSource.PlayClipAtPoint(m_ItemUseSE, new Vector3(20, 20, 0), mVolume);
        }
        private void �T���h�C�b�`()
        {
            m_OriginalObject.localScale = m_OriginalScale * m_Scale;
            Vector3 spawnPosition = transform.position - new Vector3(3, 0, 0);
            Instantiate(m_ScaleChangeEffect,spawnPosition, Quaternion.identity);
            isScaling = true;
            for (int i = 0; i < 3; i++) // 3��J��Ԃ�
            {
                m_CarMoveSpeed += 3;
                isOriginalSpeed = true;
            }
            AudioSource.PlayClipAtPoint(m_ItemUseSE,new Vector3(20, 20, 0), mVolume);
        }

    }
}