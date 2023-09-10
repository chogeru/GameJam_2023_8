using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
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
    //�T���h�C�b�`�J�E���g
    private float m_CntSand = 0;

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

    [SerializeField, Header("�A�C�e���g�p���̃T�E���h")]
    private GameObject m_ItemSE;
    [SerializeField, Header("�X�s�[�h�A�b�v�p�G�t�F�N�g")]
    private GameObject m_SpeedUpEffect;
    private bool isEffectActive=false;
    private float m_EffectFalseTime = 1.5f;
    private float m_EffectCoolTime;

    private Transform m_PlayerTransfrom;
    private Rigidbody rd;
    private void Start()
    {
        rinneitem = GetComponent<rinneItem>();
        //wara = GameObject.Find("MeganeWara");
        //ki = GameObject.Find("MeganeKi");
        //renga = GameObject.Find("MeganeRenga");
        m_motoMaxSpeed = m_MaxSpeed;
        m_SpeedUpEffect.SetActive(false);
        rd=GetComponent<Rigidbody>();
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
                    m_MaxSpeed += 3f;

                    float verticalInput = Input.GetAxis("Vertical");
                    Vector3 movement=new Vector3(0,0,verticalInput) + m_CurrentSpeed*transform.forward;
                    rd.velocity=movement;
                    /*
                    Vector3 movement = new Vector3(0, 0f, 1) * m_CurrentSpeed * 1.1f * Time.deltaTime;
                    transform.Translate(movement);*/

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
                Vector3 movement = new Vector3(0, 0, verticalInput) + m_CurrentSpeed * transform.forward;
                rd.velocity = movement;
                if (m_CntSand >= 3) m_CntSand = 0;
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
                        if(m_MaxSpeed < 30)
                        {
                            m_ItemSE.SetActive(true);
                            m_SpeedUpEffect.SetActive(true);isEffectActive= true;
                            m_MaxSpeed += 4;
                            m_motoMaxSpeed = m_MaxSpeed;
                        }
                        ChengePlayer++;
                        ItemChecker = false;
                        rinneitem.getItem = false;
                        rinneitem.m_UIObjects[SelectItem].SetActive(false);
                        break;
                    //�T���h�C�b�`
                    case 1:
                        m_CntSand++;
                        m_ItemSE.SetActive(true);
                        m_SpeedUpEffect.SetActive(true); isEffectActive = true;
                        if (m_CntSand >= 3)
                        {
                            ItemChecker = false;
                            rinneitem.getItem = false;
                            rinneitem.m_UIObjects[SelectItem].SetActive(false);
                        }
                        isBoost = true;
                        break;
                    //�f�g�b�N�X�E�H�[�^�[
                    case 2:
                        m_ItemSE.SetActive(true);
                        m_SpeedUpEffect.SetActive(true); isEffectActive = true;
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
        if(isEffectActive)
        {
            m_EffectCoolTime += Time.deltaTime;
            if(m_EffectCoolTime >m_EffectFalseTime)
            {
                m_SpeedUpEffect.SetActive(false);
                m_ItemSE.SetActive(false);
                m_EffectCoolTime = 0;
                isEffectActive = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") && !ItemChecker)
        {
            ItemChecker = true;
            m_ItemSE.SetActive(false);
            //�T�E���h�̍Đ�
            AudioSource.PlayClipAtPoint(m_ItemGetSE, transform.position, mVolume);
            //�p�[�e�B�N���̕���
            Instantiate(m_ItemEffect.gameObject.transform);
            //m_MaxSpeed += 5;
            //m_AccelerationRate += 5;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Item") && !ItemChecker)
        {
            ItemChecker = true;
            m_ItemSE.SetActive(false);
            //�T�E���h�̍Đ�
            AudioSource.PlayClipAtPoint(m_ItemGetSE, transform.position, mVolume);
            //�p�[�e�B�N���̕���
            Instantiate(m_ItemEffect.gameObject.transform);
        }
    }
}
