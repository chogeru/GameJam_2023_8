using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField,Header("加速度のレート")]
    private float m_AccelerationRate = 5f;
    [SerializeField,Header("最大スピード")]
    private float m_MaxSpeed = 25f;
    [SerializeField,Header("ジャンプ力")]
    private float m_JumpForce = 10f;
    [SerializeField,Header("ボタン押したときの回転値")]
    private float m_RotationSpeed = 90f;
    [SerializeField,Header("現在の速度")]
    private float m_CurrentSpeed = 0f;
    // ボタンを押しているかどうかのフラグ
    private bool isAccelerating = false;
    // 地面に接地しているかのフラグ
    private bool isGrounded = true;
    [SerializeField]
    private float m_StartTime=3;
    private float m_Time = 0f;
    [SerializeField, Header("アイテム所得時のエフェクト")]
    private GameObject m_ItemEffect;
    [SerializeField, Header("アイテム所得時のSE")]
    private AudioClip m_ItemGetSE;
    private float mVolume = 1;
    private void Update()
    {
        //m_Timeに加算
        m_Time += Time.deltaTime;
        //m_StartTimeよりm_Timeの値が大きくなったら
        if (m_Time > m_StartTime)
        {
            // 地面に接地しているかの判定
            //下方向にレイ(ビームみたいなもの)を飛ばして衝突しているかを判定
            //Vector3.downで下方向にレイを飛ばして判定させている、Vector.upにすると上方向に飛ぶが今回は多分使わない
            //0.5fは飛ばすレイの長さである。0.5f以内であれば、衝突していると判定してisGroundedをtrueに
            //0.5f以内でなければfalseになる
            //https://www.sejuku.net/blog/83620
            isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.5f);

            // キー入力による加速・減速制御
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                //ボタンを押しているかどうかのフラグをtrueにする
                isAccelerating = true;
            }
            else
            {
                //もしボタンが押されていない場合
                //ボタンを押しているかどうかのフラグをfalseにする
                isAccelerating = false;
                //現在の速度を加速度の速度を減算
                //ボタンを離した時に速度を減算する感じ
                //Mathf.Maxの使い方
                //https://sunagitsune.com/unitymathfmax/
                m_CurrentSpeed = Mathf.Max(m_CurrentSpeed - m_AccelerationRate * Time.deltaTime, 0f);
            }

            // 加速度の計算
            if (isAccelerating)
            {
                //キー入力した際にisAccelerating = true;でこの処理が呼び出される
                //現在の速度を加速度の速度を加算
                //ボタンを押した時に速度を加算する感じ
                //m_AccelerationRateの値を増やすと加速が早くなるよ
                m_CurrentSpeed = Mathf.Min(m_CurrentSpeed + m_AccelerationRate * Time.deltaTime, m_MaxSpeed);
            }

            // ジャンプ処理
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                //プレイヤーが地面に接触していてさらにスペースキーが押されていたらこの処理が呼び出される
                //AddFroceを使用してRigidbodyに上方向に力を与えている。(Vector3.up)のところ
                //また m_JumpForceはジャンプ力の事で、ここの数値が増えればジャンプ力が大きくなる
                //ForceMode.Impulseの記事
                //https://docs.unity3d.com/ja/current/ScriptReference/ForceMode.Impulse.html
                //分かりやすく説明すると瞬間的に力を加える
                GetComponent<Rigidbody>().AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
            }

            // キー入力による移動処理
            //水平方向の入力に使用するUnityの関数
            //https://tech.pjin.jp/blog/2021/01/13/unity_csharp_getaxis/
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            //移動ベクトルを計算、horizontalInputは水平方向の入力でverticalInputは垂直方向の入力、Y軸は使用していないため0に
            //m_CurrentSpeedは現在の速度の変数で移動ベクトルとかけることで移動を行う
            Vector3 movement = new Vector3(verticalInput, 0f, horizontalInput) * m_CurrentSpeed * Time.deltaTime;
            //上で計算されたベクトルを使用して移動させている
            //https://webst8.com/blog/css-transform-translate/
            transform.Translate(movement);

            // 回転処理
            if (Input.GetKey(KeyCode.S))
            {
                //Aキーの入力で反時計回りに回転させる
                transform.Rotate(Vector3.up, -m_RotationSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                //Dキーの入力で時計回りに回転させている
                transform.Rotate(Vector3.up, m_RotationSpeed * Time.deltaTime);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            //サウンドの再生
            AudioSource.PlayClipAtPoint(m_ItemGetSE, transform.position, mVolume);
            //パーティクルの複製
            Instantiate(m_ItemEffect.gameObject.transform);
            Destroy(other.gameObject);
            m_MaxSpeed *= 2;
            m_AccelerationRate *= 2;
        }
    }
}
