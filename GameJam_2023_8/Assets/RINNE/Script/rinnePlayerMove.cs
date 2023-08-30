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

    [Header("�A�C�e���v���n�u")]
    [SerializeField] private GameObject[] itemPrefab;
    int SelectItem;
    bool ItemChecker = false;

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

        //�A�C�e������
        if(Input.GetKey(KeyCode.F))
        {
            if(ItemChecker)
            {
                //�A�C�e�����o��
                //�A�C�e���g�p�ŃA�C�e�����o���Ȃ��悤�ɂ���
                ItemChecker = false;
                switch (SelectItem)
                {
                    case 0:
                        GameObject Item = Instantiate(itemPrefab[SelectItem]);
                        Vector3 a = new Vector3(0, 0, 6);
                        Item.transform.position = transform.localPosition+a;
                        break;
                    case 1:
                        if(m_MaxSpeed < 45f)
                        {
                            m_MaxSpeed += 10f;
                        }
                        break;
                }  
            }
            else
            {
                //�������Ȃ�
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //�����_���ȃA�C�e�����擾����
        if (other.CompareTag("Item") && !ItemChecker)
        {
            ItemChecker = true;
            SelectItem = Random.Range(0, 2);
        }
    }
}
