using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartCountDown : MonoBehaviour
{
    // �J�E���g�_�E����\������e�L�X�g�I�u�W�F�N�g
    [SerializeField]
    private Text m_CountdownText;
    // �J�E���g�_�E���̎���
    private float m_CountDownDuration = 3f;

    [SerializeField]
    private GameObject m_CountDownSE;
    [SerializeField]
    private GameObject m_StartSE;
    private void Start()
    {
        // �J�E���g�_�E�����J�n����
        StartCountdown();
    }

    private void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private System.Collections.IEnumerator CountdownCoroutine()
    {
        // �J�E���g�_�E���̏����l(3�͉��u��)
        int countdownValue = 3;

        // �J�E���g�_�E����0�ɂȂ�܂ŌJ��Ԃ�
        while (countdownValue > 0)
        {
            // �e�L�X�g�ɃJ�E���g�_�E���̌��݂̒l��\��
            m_CountdownText.text = countdownValue.ToString();
            m_CountDownSE.SetActive(true);
            // 1�b�X�g�b�v
            yield return new WaitForSeconds(1f);
            m_CountDownSE.SetActive(false);
            // �J�E���g�_�E���̒l��1���炷
            countdownValue--;
        }

        // �J�E���g�_�E���I����̏���
        m_CountdownText.text = "GO!";
        m_StartSE.SetActive(true);
        // �P�b�X�g�b�v
        yield return new WaitForSeconds(1f);
        //�e�L�X�g���\���ɂ���
        m_CountdownText.gameObject.SetActive(false);
    }
}
