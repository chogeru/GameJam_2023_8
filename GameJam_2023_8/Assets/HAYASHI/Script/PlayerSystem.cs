using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    [SerializeField, Header("加速度のレート")]
    private float m_AccelerationRate = 1f;
    [SerializeField, Header("最大スピード")]
    private float m_MaxSpeed = 25f;
    [SerializeField, Header("ジャンプ力")]
    private float m_JumpForce = 10f;
    [SerializeField, Header("ボタン押したときの回転値")]
    private float m_RotationSpeed = 90f;
    [SerializeField, Header("現在の速度")]
    private float m_CurrentSpeed = 0f;
    private bool isAccelerating = false;
    private bool isGrounded = true;
    int i = 0;
    [SerializeField, Header("アイテム所得時のエフェクト")]
    private GameObject m_ItemEffect;
    [SerializeField, Header("アイテム所得時のSE")]
    private AudioClip m_ItemGetSE;
    private float mVolume = 1;
    private float m_StartTime = 4.0f;
    private float m_Time;
    //ブーストフラグ
    private bool isBoost = false;
    //ブースト時間
    private float m_BoostTime = 0;
    //元最大スピード
    private float m_motoMaxSpeed;
    //サンドイッチカウント
    private float m_CntSand = 0;

    //アイテム取得フラグ
    public bool ItemChecker = false;
    rinneItem rinneitem;
    public int SelectItem;

    //パワーアップによるプレイヤーの見た目
    public int ChengePlayer;
    [SerializeField]
    private GameObject wara;
    [SerializeField]
    private GameObject ki;
    [SerializeField]
    private GameObject renga;
    Vector3 movement;
    public int count;

    [SerializeField, Header("アイテム使用時のサウンド")]
    private GameObject m_ItemSE;
    [SerializeField, Header("スピードアップ用エフェクト")]
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
            m_CurrentSpeed = Mathf.Min(m_CurrentSpeed + m_AccelerationRate * Time.deltaTime, m_MaxSpeed); // 最大速度を超えないように制限

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
                    //スイカ
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
                    //サンドイッチ
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
                    //デトックスウォーター
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
                //何もしない
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
            //サウンドの再生
            AudioSource.PlayClipAtPoint(m_ItemGetSE, transform.position, mVolume);
            //パーティクルの複製
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
            //サウンドの再生
            AudioSource.PlayClipAtPoint(m_ItemGetSE, transform.position, mVolume);
            //パーティクルの複製
            Instantiate(m_ItemEffect.gameObject.transform);
        }
    }
}
