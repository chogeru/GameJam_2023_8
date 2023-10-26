using UnityEngine;

public class PlayerRespown : MonoBehaviour
{
    //最後に通過したチェックポイントの座標
    private Vector3 m_LastCheckPointPosition;
    //最後に通過したチェックポイントの回転;
    private Quaternion m_LastCheckPointRotation;

    private void Start()
    {
        m_LastCheckPointPosition = transform.position;
        m_LastCheckPointRotation = transform.rotation;
    }

    private void Update()
    {
        if (transform.position.y < -10f)
        {
            // 最後のCheckPointの位置と回転に戻す
            transform.position = m_LastCheckPointPosition;
            transform.rotation = m_LastCheckPointRotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            m_LastCheckPointPosition = other.transform.position;
            m_LastCheckPointRotation = other.transform.rotation; // チェックポイントの回転を保存
        }
    }
}
