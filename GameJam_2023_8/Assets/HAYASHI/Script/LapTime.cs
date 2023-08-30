using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LapTime : MonoBehaviour
{
    private float m_StartTime;
    private bool isTiming = false;
    public Text m_TimerText; // UI�e�L�X�g�I�u�W�F�N�g���A�^�b�`����
    private void Start()
    {
        // �Q�[���J�n���ɃX�^�[�g���Ԃ��L�^
        m_StartTime = Time.timeSinceLevelLoad;
        isTiming = true;
    }

    private void Update()
    {
        if (isTiming)
        {
            // ���݂̎��Ԃ���X�^�[�g���Ԃ������āA�o�ߎ��Ԃ��v�Z
            float elapsedTime = Time.timeSinceLevelLoad - m_StartTime;

           
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);

            // ���Ԃ��e�L�X�g�\��
            string timerTextString = string.Format("Time:{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
            // UI�e�L�X�g�ɕ\��
            m_TimerText.text = timerTextString; 
        }
    }

    public void StopTimer()
    {
        // �^�C�}�[���~
        isTiming = false;
    }
}
