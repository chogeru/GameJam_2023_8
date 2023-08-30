using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rinnePlayerMove : MonoBehaviour
{
    [SerializeField, Header("加速度のレート")]
    private float m_AccelerationRate = 5f;
    [SerializeField, Header("最大スピード")]
    private float m_MaxSpeed = 25f;
    [SerializeField, Header("ジャンプ力")]
    private float m_JumpForce = 10f;
    [SerializeField, Header("ボタン押したときの回転値")]
    private float m_RotationSpeed = 90f;
    [SerializeField, Header("現在の速度")]
    private float m_CurrentSpeed = 0f;

    [Header("アイテムプレハブ")]
    [SerializeField] private GameObject[] itemPrefab;
    int SelectItem;
    bool ItemChecker = false;

    // ボタンを押しているかどうかのフラグ
    private bool isAccelerating = false;
    // 地面に接地しているかのフラグ
    private bool isGrounded = true;


    

    private void Update()
    {
        // 地面に接地しているかの判定
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.10f);

        // キー入力による加速・減速制御
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isAccelerating = true;
        }
        else
        {
            isAccelerating = false;
            m_CurrentSpeed = Mathf.Max(m_CurrentSpeed - m_AccelerationRate * Time.deltaTime, 0f); // ボタンを離したら減速
        }

        // 加速度の計算
        if (isAccelerating)
        {
            m_CurrentSpeed = Mathf.Min(m_CurrentSpeed + m_AccelerationRate * Time.deltaTime, m_MaxSpeed); // 最大速度を超えないように制限
        }

        // ジャンプ処理
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
        }

        // キー入力による移動処理
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * m_CurrentSpeed * Time.deltaTime;
        transform.Translate(movement);

        // 回転処理
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -m_RotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, m_RotationSpeed * Time.deltaTime);
        }

        //アイテム処理
        if(Input.GetKey(KeyCode.F))
        {
            if(ItemChecker)
            {
                //アイテムを出す
                //アイテム使用でアイテムを出せないようにする
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
                //何もしない
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //ランダムなアイテムを取得する
        if (other.CompareTag("Item") && !ItemChecker)
        {
            ItemChecker = true;
            SelectItem = Random.Range(0, 2);
        }
    }
}
