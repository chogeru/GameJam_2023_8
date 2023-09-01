using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
namespace HAYASHI.Script
{

    public class NPCCarMove : MonoBehaviour
    {
        // 目的地のオブジェクトを配列で保持
        [SerializeField]
        private Transform[] m_Destinations;
        // 車の移動速度
        [SerializeField]
        private float m_CarMoveSpeed = 25f;
        // 現在の目的地のインデックス
        private int m_CurrentDestinationIndex = 0;

        [SerializeField]
        private float m_StartTimer = 7;
        [SerializeField]
        private float m_Timer;
        private bool isStart = false;

        [SerializeField, Header("アイテム所得時のエフェクト")]
        private GameObject m_ItemEffect;

        [SerializeField, Header("アイテム所得時のSE")]
        private AudioClip m_ItemGetSE;
        private float mVolume = 1;
        [SerializeField, Header("アイテム使用時のSE")]
        private AudioClip m_ItemUseSE;

        [SerializeField]
        private bool isItemSuika = false;
        [SerializeField]
        private bool isItemWater = false;
        [SerializeField]
        private bool isItemSando = false;

        private float m_MinInterval = 2.0f;
        private float m_MaxInterval = 5.0f;

        //元の速度に戻す際のトリガー
        private bool isOriginalSpeed = false;

        private float m_OriginalSpeedTime = 3;
        private float m_CoolTime;

        //車の現在のグレード
        private int m_CarGrade = 0;

        //車のオブジェクト
        [SerializeField]
        private GameObject m_GreadWara;
        [SerializeField]
        private GameObject m_GreadKI;
        [SerializeField]
        private GameObject m_GreadRenga;

        private float m_PlusCarSpeedKI;

        [SerializeField]
        private Transform m_OriginalObject;
        private float m_Scale = 2f;
        private float m_ScaleUpTime = 10f;

        private Vector3 m_OriginalScale;
        [SerializeField]
        private bool isScaling = false;
        private float m_ScaleCoolTime;

        [SerializeField,Header("巨大化とパワーアップ時のエフェクト")]
        private GameObject m_ScaleChangeEffect;
        private void Start()
        {
            m_CarMoveSpeed = 0;
            MoveToDestination(m_CurrentDestinationIndex);
            m_OriginalScale = m_OriginalObject.localScale;
        }
        private void Update()
        {
            
            //変数の値の範囲を指定
            m_CarGrade = Mathf.Clamp(m_CarGrade, 0, 3);
            if (isStart == false)
            {
                m_Timer += Time.deltaTime;
                if (m_StartTimer < m_Timer)
                {
                    m_CarMoveSpeed = 25;
                    m_Timer = 0;
                    isStart = true;
                }
            }
            if (isItemSando)
            {
                // ランダムな時間設定
                float randomInterval = Random.Range(m_MinInterval, m_MaxInterval);
                //上記の処理で決まったランダムな時間経過で関数を呼び出す
                Invoke("サンドイッチ", randomInterval);
                isItemSando = false;
            }
            if (isItemSuika)
            {
                // ランダムな時間設定
                float randomInterval = Random.Range(m_MinInterval, m_MaxInterval);
                Invoke("四角スイカ", randomInterval);
                isItemSuika = false;
            }
            if (isItemWater)
            {
                // ランダムな時間設定
                float randomInterval = Random.Range(m_MinInterval, m_MaxInterval);
                Invoke("デトックスウォーター", randomInterval);
                isItemWater = false;
            }
            //時間経過で元のスピードに戻す処理
            if (isOriginalSpeed)
            {
                m_CoolTime += Time.deltaTime;
                if (m_CoolTime > m_OriginalSpeedTime)
                {
                    m_CarMoveSpeed = 25;
                    m_CoolTime = 0;
                    isOriginalSpeed = false;
                }
            }
            if (m_CarGrade == 0)
            {
                m_GreadWara.SetActive(true);
                m_GreadKI.SetActive(false);
                m_GreadRenga.SetActive(false);
                m_PlusCarSpeedKI = 0;
            }


            if (isScaling)
            {
                m_ScaleCoolTime += Time.deltaTime;
                if (m_ScaleCoolTime >= m_ScaleUpTime)
                {
                    m_OriginalObject.localScale = m_OriginalScale;
                    isScaling = false;
                    m_ScaleCoolTime = 0f;
                }
            }
        }
        private void MoveToDestination(int destinationIndex)
        {
            // 目的地が無効なインデックスの場合は終了
            if (destinationIndex >= m_Destinations.Length || destinationIndex < 0)
                return;

            // 目的地への移動を開始
            StartCoroutine(MoveCoroutine(m_Destinations[destinationIndex].position));
        }

        private IEnumerator MoveCoroutine(Vector3 destination)
        {
            //車両が目的地に到達するまでループ
            //車両の現在位置と目的地の距離を計算し0.05ｆ以上大きいとループする
            while (Vector3.Distance(transform.position, destination) > 0.05f)
            {
                //目的地の方向を計算
                Vector3 lookDir = destination - transform.position;
                //車両が垂直方向に回転しないようにする
                lookDir.y = 0f;
                if (lookDir != Vector3.zero)
                {
                    // 目的地の方向を向く
                    transform.rotation = Quaternion.LookRotation(lookDir);
                }

                // 目的地への移動
                transform.position = Vector3.MoveTowards(transform.position, destination, m_CarMoveSpeed * Time.deltaTime);
                yield return null;
            }

            // 目的地に到着したら次の目的地へ
            m_CurrentDestinationIndex++;
            if (m_CurrentDestinationIndex >= m_Destinations.Length)
            {
                // 最後の目的地に到達したら最初に戻る
                m_CurrentDestinationIndex = 0;
            }
            // 次の目的地へ移動
            MoveToDestination(m_CurrentDestinationIndex);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Item"))
            {
                // ランダムに0から2の整数を生成
                int randomIndex = Random.Range(0, 3);
                //サウンドの再生
                AudioSource.PlayClipAtPoint(m_ItemGetSE,new Vector3(20,20,0), mVolume);
                //パーティクルの複製
                Instantiate(m_ItemEffect.gameObject.transform);
                // ランダムなアイテム用関数を呼び出す
                switch (randomIndex)
                {
                    case 0:
                        //時間経過で関数を呼び出す用のトリガー
                        isItemSuika = true;
                        break;
                    case 1:
                        isItemWater = true;
                        break;
                    case 2:
                        isItemSando = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void デトックスウォーター()
        {
            m_CarMoveSpeed += 7;
            isOriginalSpeed = true;
            AudioSource.PlayClipAtPoint(m_ItemUseSE, new Vector3(20, 20, 0), mVolume);
        }
        private void 四角スイカ()
        {
            m_CarGrade += 1;
            Vector3 spawnPosition = transform.position - new Vector3(3, 0, 0);
            if (m_CarGrade == 1)
            {
                m_GreadWara.SetActive(false);
                m_GreadKI.SetActive(true);
                m_GreadRenga.SetActive(false);
                m_PlusCarSpeedKI = 3f;
                m_CarMoveSpeed += m_PlusCarSpeedKI;
                Instantiate(m_ScaleChangeEffect, spawnPosition, Quaternion.identity);
            }
            if (m_CarGrade == 2)
            {
                m_GreadWara.SetActive(false);
                m_GreadKI.SetActive(false);
                m_GreadRenga.SetActive(true);
                Instantiate(m_ScaleChangeEffect, spawnPosition, Quaternion.identity);
            }
            AudioSource.PlayClipAtPoint(m_ItemUseSE, new Vector3(20, 20, 0), mVolume);
        }
        private void サンドイッチ()
        {
            m_OriginalObject.localScale = m_OriginalScale * m_Scale;
            Vector3 spawnPosition = transform.position - new Vector3(3, 0, 0);
            Instantiate(m_ScaleChangeEffect,spawnPosition, Quaternion.identity);
            isScaling = true;
            for (int i = 0; i < 3; i++) // 3回繰り返す
            {
                m_CarMoveSpeed += 3;
                isOriginalSpeed = true;
            }
            AudioSource.PlayClipAtPoint(m_ItemUseSE,new Vector3(20, 20, 0), mVolume);
        }

    }
}