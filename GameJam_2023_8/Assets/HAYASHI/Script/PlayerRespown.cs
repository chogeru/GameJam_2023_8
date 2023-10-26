using UnityEngine;

public class PlayerRespown : MonoBehaviour
{
    //�Ō�ɒʉ߂����`�F�b�N�|�C���g�̍��W
    private Vector3 m_LastCheckPointPosition;
    //�Ō�ɒʉ߂����`�F�b�N�|�C���g�̉�];
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
            // �Ō��CheckPoint�̈ʒu�Ɖ�]�ɖ߂�
            transform.position = m_LastCheckPointPosition;
            transform.rotation = m_LastCheckPointRotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            m_LastCheckPointPosition = other.transform.position;
            m_LastCheckPointRotation = other.transform.rotation; // �`�F�b�N�|�C���g�̉�]��ۑ�
        }
    }
}
