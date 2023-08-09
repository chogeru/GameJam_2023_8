using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 加速度のレート
    [SerializeField]
    private float m_AccelerationRate = 10f;
    // 最大速度
    [SerializeField]
    private float m_MaxSpeed = 25f; 
    [SerializeField]
    private float m_CurrentSpeed = 0f; // 現在の速度
    [SerializeField]
    private bool isAccelerating = false; // ボタンを押しているかどうかのフラグ

    private void Update()
    {
        // キー入力による加速・減速制御
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isAccelerating = true;
        }
        else
        {
            isAccelerating = false;
            m_CurrentSpeed = Mathf.Max(m_CurrentSpeed - m_AccelerationRate * Time.deltaTime, 5f); // ボタンを離したら減速
        }

        // 加速度の計算
        if (isAccelerating)
        {
            m_CurrentSpeed = Mathf.Min(m_CurrentSpeed + m_AccelerationRate * Time.deltaTime, m_MaxSpeed); // 最大速度を超えないように制限
        }

        // キー入力による移動処理
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * m_CurrentSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
}
