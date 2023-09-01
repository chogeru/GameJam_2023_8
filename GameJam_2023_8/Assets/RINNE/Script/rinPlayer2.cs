using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rinPlayer2 : MonoBehaviour
{
    [SerializeField, Header("�����x�̃��[�g")]
    private float m_AccelerationRate = 1f;
    [SerializeField, Header("�ő�X�s�[�h")]
    private float m_MaxSpeed = 25f;
    [SerializeField, Header("�W�����v��")]
    private float m_JumpForce = 10f;
    [SerializeField, Header("�{�^���������Ƃ��̉�]�l")]
    private float m_RotationSpeed = 90f;
    [SerializeField, Header("���݂̑��x")]
    private float m_CurrentSpeed = 0f;
    private bool isAccelerating = false;
    private bool isGrounded = true;
    int i = 0;
    [SerializeField, Header("�A�C�e���������̃G�t�F�N�g")]
    private GameObject m_ItemEffect;
    [SerializeField, Header("�A�C�e����������SE")]
    private AudioClip m_ItemGetSE;
    private float mVolume = 1;
    private float m_StartTime = 4.0f;
    private float m_Time;
    //�u�[�X�g�t���O
    private bool isBoost = false;
    //�u�[�X�g����
    private float m_BoostTime = 0;
    //���ő�X�s�[�h
    private float m_motoMaxSpeed;

    //�A�C�e���擾�t���O
    public bool ItemChecker = false;
    rinneItem rinneitem;
    public int SelectItem;

    //�p���[�A�b�v�ɂ��v���C���[�̌�����
    public int ChengePlayer;
    [SerializeField]
    private GameObject wara;
    [SerializeField]
    private GameObject ki;
    [SerializeField]
    private GameObject renga;
    Vector3 movement;
    public int count;

    private void Start()
    {
        rinneitem = GetComponent<rinneItem>();
        //wara = GameObject.Find("MeganeWara");
        //ki = GameObject.Find("MeganeKi");
        //renga = GameObject.Find("MeganeRenga");
        m_motoMaxSpeed = m_MaxSpeed;
    }
    void Update()
    {
        m_Time += Time.deltaTime;
        if (m_Time > m_StartTime)
        {
            m_CurrentSpeed = Mathf.Min(m_CurrentSpeed + m_AccelerationRate * Time.deltaTime, m_MaxSpeed); // �ő呬�x�𒴂��Ȃ��悤�ɐ���

            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
            }

            if (isBoost)
            {
                m_BoostTime += Time.deltaTime;
                if (m_BoostTime < 1.5f)
                {
                    m_MaxSpeed += 10f;

                    float verticalInput = Input.GetAxis("Vertical");
                    Vector3 movement = new Vector3(0, 0f, 1) * m_CurrentSpeed * 2f * Time.deltaTime;
                    transform.Translate(movement);

                }
                else
                {
                    m_BoostTime = 0;
                    isBoost = false;
                }
            }
            else
            {
                m_MaxSpeed = m_motoMaxSpeed;
                float verticalInput = Input.GetAxis("Vertical");
                Vector3 movement = new Vector3(0, 0f, 1) * m_CurrentSpeed * Time.deltaTime;
                transform.Translate(movement);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.up, -m_RotationSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up, m_RotationSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (ItemChecker && !rinneitem.isCanActivateUI)
            {
                switch(SelectItem)
                {
                    //�X�C�J
                    case 0:
                        m_MaxSpeed += 10;
                        ChengePlayer++;
                        ItemChecker = false;
                        rinneitem.getItem = false;
                        rinneitem.m_UIObjects[SelectItem].SetActive(false);
                        break;
                    //�T���h�C�b�`
                    case 1:
                        //m_MaxSpeed += 10;
                        ItemChecker = false;
                        rinneitem.getItem = false;
                        rinneitem.m_UIObjects[SelectItem].SetActive(false);
                        break;
                    //�f�g�b�N�X�E�H�[�^�[
                    case 2:
                        isBoost = true;
                        ItemChecker = false;
                        rinneitem.getItem = false;
                        rinneitem.m_UIObjects[SelectItem].SetActive(false);
                        break;
                }
            }
            else
            {
                //�������Ȃ�
            }
        }
        SelectItem = rinneitem.randomIndex;
        switch(ChengePlayer)
        {
            case 0:
                wara.SetActive(true);
                ki.SetActive(false);
                renga.SetActive(false);
                break;
            case 1:
                wara.SetActive(false);
                ki.SetActive(true);
                renga.SetActive(false);
                break;
            case 2:
                wara.SetActive(false);
                ki.SetActive(false);
                renga.SetActive(true);
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") && !ItemChecker)
        {
            ItemChecker = true;
            //�T�E���h�̍Đ�
            AudioSource.PlayClipAtPoint(m_ItemGetSE, transform.position, mVolume);
            //�p�[�e�B�N���̕���
            Instantiate(m_ItemEffect.gameObject.transform);
            //m_MaxSpeed += 5;
            //m_AccelerationRate += 5;
        }
    }
}
