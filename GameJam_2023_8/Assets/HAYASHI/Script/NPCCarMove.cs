using System.Collections;
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
        private float m_StartTimer= 7;
        [SerializeField]
        private float m_Timer;
        private bool isStart=false;
        [SerializeField, Header("�A�C�e���������̃G�t�F�N�g")]
        private GameObject m_ItemEffect;
        [SerializeField, Header("�A�C�e����������SE")]
        private AudioClip m_ItemGetSE;
        private float mVolume = 1;
        private void Start()
        {
            m_CarMoveSpeed = 0;
            MoveToDestination(m_CurrentDestinationIndex);
        }
        private void Update()
        {
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
                //���ꂪ�Ȃ��Ǝԗ��̋������o�O��܂�
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
                //�T�E���h�̍Đ�
                AudioSource.PlayClipAtPoint(m_ItemGetSE, transform.position, mVolume);
                //�p�[�e�B�N���̕���
                Instantiate(m_ItemEffect.gameObject.transform);
                Destroy(other.gameObject);
                m_CarMoveSpeed +=10;
            }
        }
    }

}