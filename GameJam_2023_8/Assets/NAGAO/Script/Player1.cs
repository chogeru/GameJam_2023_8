using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
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

    //アイテム取得フラグ
    bool ItemChecker = false;
    ItemBoxAnimeTion itemboxAnimetion;
    //UIで抽選されたアイテム番号
    public int SelectItem;

    private void Start()
    {
        itemboxAnimetion = GetComponent<ItemBoxAnimeTion>();
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


            float verticalInput = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(0, 0f, 1) * m_CurrentSpeed * Time.deltaTime;
            transform.Translate(movement);


            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.up, -m_RotationSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up, m_RotationSpeed * Time.deltaTime);
            }
        }

        if(Input.GetKey(KeyCode.E))
        {
            if(ItemChecker && !itemboxAnimetion.isCanActivateUI)
            {
                switch(SelectItem)
                {
                    //スイカ
                    case 0:
                        if(m_MaxSpeed < 45)
                        {
                        m_MaxSpeed += 10;
                        }
                        ItemChecker = false;
                        itemboxAnimetion.m_UIObjects[SelectItem].SetActive(false);
                        break;
                    //サンドイッチ
                    case 1:
                        ItemChecker = false;
                        itemboxAnimetion.m_UIObjects[SelectItem].SetActive(false);
                        break;
                    //デトックスウォーター
                    case 2:
                        ItemChecker = false;
                        itemboxAnimetion.m_UIObjects[SelectItem].SetActive(false);
                        break;
                }
            }
        }
        SelectItem = itemboxAnimetion.randomIndex;
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
