using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rinPlayer2 : MonoBehaviour
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
                    //スイカ
                    case 0:
                        m_MaxSpeed += 10;
                        ChengePlayer++;
                        ItemChecker = false;
                        rinneitem.getItem = false;
                        rinneitem.m_UIObjects[SelectItem].SetActive(false);
                        break;
                    //サンドイッチ
                    case 1:
                        //m_MaxSpeed += 10;
                        ItemChecker = false;
                        rinneitem.getItem = false;
                        rinneitem.m_UIObjects[SelectItem].SetActive(false);
                        break;
                    //デトックスウォーター
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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") && !ItemChecker)
        {
            ItemChecker = true;
            //サウンドの再生
            AudioSource.PlayClipAtPoint(m_ItemGetSE, transform.position, mVolume);
            //パーティクルの複製
            Instantiate(m_ItemEffect.gameObject.transform);
            //m_MaxSpeed += 5;
            //m_AccelerationRate += 5;
        }
    }
}
