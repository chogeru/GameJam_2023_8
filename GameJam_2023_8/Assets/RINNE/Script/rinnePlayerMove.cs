using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rinnePlayerMove : MonoBehaviour
{
    [SerializeField, Header("�����x�̃��[�g")]
    private float m_AccelerationRate = 5f;
    [SerializeField, Header("�ő�X�s�[�h")]
    private float m_MaxSpeed = 25f;
    [SerializeField, Header("�W�����v��")]
    private float m_JumpForce = 10f;
    [SerializeField, Header("�{�^���������Ƃ��̉�]�l")]
    private float m_RotationSpeed = 90f;
    [SerializeField, Header("���݂̑��x")]
    private float m_CurrentSpeed = 0f;
    // �{�^���������Ă��邩�ǂ����̃t���O
    private bool isAccelerating = false;
    // �n�ʂɐڒn���Ă��邩�̃t���O
    private bool isGrounded = true;

    

    private void Update()
    {
        // �n�ʂɐڒn���Ă��邩�̔���
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.10f);

        // �L�[���͂ɂ������E��������
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isAccelerating = true;
        }
        else
        {
            isAccelerating = false;
            m_CurrentSpeed = Mathf.Max(m_CurrentSpeed - m_AccelerationRate * Time.deltaTime, 0f); // �{�^���𗣂����猸��
        }

        // �����x�̌v�Z
        if (isAccelerating)
        {
            m_CurrentSpeed = Mathf.Min(m_CurrentSpeed + m_AccelerationRate * Time.deltaTime, m_MaxSpeed); // �ő呬�x�𒴂��Ȃ��悤�ɐ���
        }

        // �W�����v����
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
        }

        // �L�[���͂ɂ��ړ�����
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * m_CurrentSpeed * Time.deltaTime;
        transform.Translate(movement);

        // ��]����
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -m_RotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, m_RotationSpeed * Time.deltaTime);
        }

        //���ʃx�N�g�����擾
        var forward = transform.forward;

        //if (Input.GetKey(KeyCode.T))
        //{
        //    Instantiate(item);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            
        }
    }
}
