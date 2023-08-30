using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LapTime : MonoBehaviour
{
    private float m_StartTime;
    private bool isTiming = false;
    public Text m_TimerText; // UIテキストオブジェクトをアタッチする
    private void Start()
    {
        // ゲーム開始時にスタート時間を記録
        m_StartTime = Time.timeSinceLevelLoad;
        isTiming = true;
    }

    private void Update()
    {
        if (isTiming)
        {
            // 現在の時間からスタート時間を引いて、経過時間を計算
            float elapsedTime = Time.timeSinceLevelLoad - m_StartTime;

           
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);

            // 時間をテキスト表示
            string timerTextString = string.Format("Time:{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
            // UIテキストに表示
            m_TimerText.text = timerTextString; 
        }
    }

    public void StopTimer()
    {
        // タイマーを停止
        isTiming = false;
    }
}
