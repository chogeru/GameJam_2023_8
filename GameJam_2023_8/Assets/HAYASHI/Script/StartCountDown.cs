using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartCountDown : MonoBehaviour
{
    // カウントダウンを表示するテキストオブジェクト
    [SerializeField]
    private Text m_CountdownText;
    // カウントダウンの時間
    private float m_CountDownDuration = 3f;

    [SerializeField]
    private GameObject m_CountDownSE;
    [SerializeField]
    private GameObject m_StartSE;
    private void Start()
    {
        // カウントダウンを開始する
        StartCountdown();
    }

    private void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private System.Collections.IEnumerator CountdownCoroutine()
    {
        // カウントダウンの初期値(3は仮置き)
        int countdownValue = 3;

        // カウントダウンが0になるまで繰り返す
        while (countdownValue > 0)
        {
            // テキストにカウントダウンの現在の値を表示
            m_CountdownText.text = countdownValue.ToString();
            m_CountDownSE.SetActive(true);
            // 1秒ストップ
            yield return new WaitForSeconds(1f);
            m_CountDownSE.SetActive(false);
            // カウントダウンの値を1減らす
            countdownValue--;
        }

        // カウントダウン終了後の処理
        m_CountdownText.text = "GO!";
        m_StartSE.SetActive(true);
        // １秒ストップ
        yield return new WaitForSeconds(1f);
        //テキストを非表示にする
        m_CountdownText.gameObject.SetActive(false);
    }
}
