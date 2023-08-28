using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField,Header("�����x�̃��[�g")]
    private float m_AccelerationRate = 5f;
    [SerializeField,Header("�ő�X�s�[�h")]
    private float m_MaxSpeed = 25f;
    [SerializeField,Header("�W�����v��")]
    private float m_JumpForce = 10f;
    [SerializeField,Header("�{�^���������Ƃ��̉�]�l")]
    private float m_RotationSpeed = 90f;
    [SerializeField,Header("���݂̑��x")]
    private float m_CurrentSpeed = 0f;
    // �{�^���������Ă��邩�ǂ����̃t���O
    private bool isAccelerating = false;
    // �n�ʂɐڒn���Ă��邩�̃t���O
    private bool isGrounded = true;
    [SerializeField]
    private float m_StartTime=3;
    private float m_Time = 0f;
    [SerializeField, Header("�A�C�e���������̃G�t�F�N�g")]
    private GameObject m_ItemEffect;
    [SerializeField, Header("�A�C�e����������SE")]
    private AudioClip m_ItemGetSE;
    private float mVolume = 1;
    private void Update()
    {
        //m_Time�ɉ��Z
        m_Time += Time.deltaTime;
        //m_StartTime���m_Time�̒l���傫���Ȃ�����
        if (m_Time > m_StartTime)
        {
            // �n�ʂɐڒn���Ă��邩�̔���
            //�������Ƀ��C(�r�[���݂����Ȃ���)���΂��ďՓ˂��Ă��邩�𔻒�
            //Vector3.down�ŉ������Ƀ��C���΂��Ĕ��肳���Ă���AVector.up�ɂ���Ə�����ɔ�Ԃ�����͑����g��Ȃ�
            //0.5f�͔�΂����C�̒����ł���B0.5f�ȓ��ł���΁A�Փ˂��Ă���Ɣ��肵��isGrounded��true��
            //0.5f�ȓ��łȂ����false�ɂȂ�
            //https://www.sejuku.net/blog/83620
            isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.5f);

            // �L�[���͂ɂ������E��������
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                //�{�^���������Ă��邩�ǂ����̃t���O��true�ɂ���
                isAccelerating = true;
            }
            else
            {
                //�����{�^����������Ă��Ȃ��ꍇ
                //�{�^���������Ă��邩�ǂ����̃t���O��false�ɂ���
                isAccelerating = false;
                //���݂̑��x�������x�̑��x�����Z
                //�{�^���𗣂������ɑ��x�����Z���銴��
                //Mathf.Max�̎g����
                //https://sunagitsune.com/unitymathfmax/
                m_CurrentSpeed = Mathf.Max(m_CurrentSpeed - m_AccelerationRate * Time.deltaTime, 0f);
            }

            // �����x�̌v�Z
            if (isAccelerating)
            {
                //�L�[���͂����ۂ�isAccelerating = true;�ł��̏������Ăяo�����
                //���݂̑��x�������x�̑��x�����Z
                //�{�^�������������ɑ��x�����Z���銴��
                //m_AccelerationRate�̒l�𑝂₷�Ɖ����������Ȃ��
                m_CurrentSpeed = Mathf.Min(m_CurrentSpeed + m_AccelerationRate * Time.deltaTime, m_MaxSpeed);
            }

            // �W�����v����
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                //�v���C���[���n�ʂɐڐG���Ă��Ă���ɃX�y�[�X�L�[��������Ă����炱�̏������Ăяo�����
                //AddFroce���g�p����Rigidbody�ɏ�����ɗ͂�^���Ă���B(Vector3.up)�̂Ƃ���
                //�܂� m_JumpForce�̓W�����v�͂̎��ŁA�����̐��l��������΃W�����v�͂��傫���Ȃ�
                //ForceMode.Impulse�̋L��
                //https://docs.unity3d.com/ja/current/ScriptReference/ForceMode.Impulse.html
                //������₷����������Əu�ԓI�ɗ͂�������
                GetComponent<Rigidbody>().AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
            }

            // �L�[���͂ɂ��ړ�����
            //���������̓��͂Ɏg�p����Unity�̊֐�
            //https://tech.pjin.jp/blog/2021/01/13/unity_csharp_getaxis/
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            //�ړ��x�N�g�����v�Z�AhorizontalInput�͐��������̓��͂�verticalInput�͐��������̓��́AY���͎g�p���Ă��Ȃ�����0��
            //m_CurrentSpeed�͌��݂̑��x�̕ϐ��ňړ��x�N�g���Ƃ����邱�Ƃňړ����s��
            Vector3 movement = new Vector3(verticalInput, 0f, horizontalInput) * m_CurrentSpeed * Time.deltaTime;
            //��Ōv�Z���ꂽ�x�N�g�����g�p���Ĉړ������Ă���
            //https://webst8.com/blog/css-transform-translate/
            transform.Translate(movement);

            // ��]����
            if (Input.GetKey(KeyCode.S))
            {
                //A�L�[�̓��͂Ŕ����v���ɉ�]������
                transform.Rotate(Vector3.up, -m_RotationSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                //D�L�[�̓��͂Ŏ��v���ɉ�]�����Ă���
                transform.Rotate(Vector3.up, m_RotationSpeed * Time.deltaTime);
            }
        }
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
            m_MaxSpeed *= 2;
            m_AccelerationRate *= 2;
        }
    }
}
