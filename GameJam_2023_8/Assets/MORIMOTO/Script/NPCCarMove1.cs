using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace HAYASHI_MORIMOTO.Script
{
    [RequireComponent(typeof(NPCCarMove1))]
    public class NPCCarMove1 : MonoBehaviour
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
        private float m_StartTimer= 7;
        [SerializeField]
        private float m_Timer;
        // Calのスクリプト
        [SerializeField]
        public Cal m_cal;
        //スケール
        [SerializeField]
        Vector3 m_Scale;
        //元スケール
        [SerializeField]
        Vector3 m_motoScale;
        //スタートフラグ
        [SerializeField]
        private bool isStart = false;
        //タイムカロリー
        [SerializeField]
        private float m_timecal;
        //Boothフラグ
        [SerializeField]
        private bool isBooth = false;

        private void Start()
        {
            //元のScale
            m_motoScale = new Vector3(1, 1, 1);
            //現在のScale
            m_Scale = transform.localScale;
            m_CarMoveSpeed = 0;
            MoveToDestination(m_CurrentDestinationIndex);
            m_timecal = 0;
        }
        private void Update()
        {
            //現在のScale値取得
            m_Scale = transform.localScale;
            m_Timer += Time.deltaTime;
            if (!isStart)
            {
                if (m_StartTimer < m_Timer)
                {
                    m_CarMoveSpeed = 25;
                    isStart = true;
                }
            }
            //元のScaleより現在のScaleが大きければ
            if (m_motoScale.x < m_Scale.x)
            {
                //だんだんScaleが元の大きさに戻る
                //transform.localScale = new Vector3(m_Scale.x - Time.deltaTime * 0.1f, m_Scale.y - Time.deltaTime * 0.1f, m_Scale.z - Time.deltaTime * 0.1f);
                
                //Spaceキーを押す
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //Scaleを一段階下げる
                    transform.localScale = new Vector3(m_Scale.x - 1.0f, m_Scale.y - 1.0f, m_Scale.z - 1.0f);
                    //ブーストフラグをON
                    isBooth = true;
                }
            }
            //ブーストフラグがONだったら
            if (isBooth)
            {
                m_timecal += Time.deltaTime;
                //右の時間までブースト
                if (m_timecal <= 1.0f)//if (m_timecal <= 1.0f)//if (m_timecal <= 2.0f)
                {
                    //加速度
                    m_CarMoveSpeed += 0.3f;//m_CarMoveSpeed += 0.2f;//m_CarMoveSpeed += 0.1f;
                }
                else
                {
                    //タイムカロリーを0に戻す
                    m_timecal = 0;
                    //ブーストフラグをOFF
                    isBooth = false;
                }
            }
            else
            {
                //スタートカウントが終わったら
                if (m_StartTimer < m_Timer)
                {
                    //Scale値が4倍の時の加速度
                    if (m_motoScale.x * 4 == m_Scale.x) {
                        m_CarMoveSpeed = 10;

                    }
                    //Scale値が3倍の時の加速度
                    else if (m_motoScale.x * 3 == m_Scale.x)
                    {
                        m_CarMoveSpeed = 15;
                    }
                    //Scale値が2倍の時の加速度
                    else if (m_motoScale.x * 2 == m_Scale.x)
                    {
                        m_CarMoveSpeed = 20;
                    }
                    //Scale値が等倍の時の加速度
                    else
                    {
                        m_CarMoveSpeed = 25;
                    }
                }
                //減速なしの場合
                //m_CarMoveSpeed = 25;
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
                //これがないと車両の挙動がバグります
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
                Destroy(other.gameObject);
                m_CarMoveSpeed *= 2;
            }
            
            //Scaleチェンジ用
            //Colliderに接触したとき
            if(other.CompareTag("Cal"))
            {
                Destroy(other.gameObject);
                //CalのChangeCal関数から値を取得する
                switch (m_cal.ChangeCal())
                {
                    case 1:
                        //Scale値2倍
                        transform.localScale = new Vector3(m_Scale.x * 2, m_Scale.y * 2, m_Scale.z * 2);
                        break;
                    case 2:
                        //Scale値3倍
                        transform.localScale = new Vector3(m_Scale.x * 3, m_Scale.y * 3, m_Scale.z * 3);
                        break;
                    case 3:
                        //Scale値4倍
                        transform.localScale = new Vector3(m_Scale.x * 4, m_Scale.y * 4, m_Scale.z * 4);
                        break;
                }

            }
        }
    }

}