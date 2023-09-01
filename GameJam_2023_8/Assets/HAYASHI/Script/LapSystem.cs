using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LapSystem : MonoBehaviour
{
    
    private Text m_CheckPointText;
    private int m_TotalCheckPoints = 3;
    private int m_CheckPointsReached = 0;
    [SerializeField]
    private string m_SceneName = "";
    //三週回り切った後のSE
    private GameObject m_ClereSE;
    private GameObject m_ClereEffect;

    private void Start()
    {
        
        m_ClereSE = GameObject.Find("クリアSE");
        m_ClereEffect = GameObject.Find("FinishEffect");
        m_ClereSE.SetActive(false);
        m_ClereEffect.SetActive(false);
        GameObject checkpointTextObject = GameObject.FindGameObjectWithTag("RapText");

        // テキストオブジェクトから Text コンポーネントを取得
        if (checkpointTextObject != null)
        {
            m_CheckPointText = checkpointTextObject.GetComponent<Text>();
        }
        UpdateCheckpointText();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckpointStart"))
        {
            m_CheckPointsReached++;
            UpdateCheckpointText();

            if (m_CheckPointsReached >= m_TotalCheckPoints)
            {
                m_ClereSE.SetActive(true);
                m_ClereEffect.SetActive(true);

                StartCoroutine(LoadSceneAfterDelay(3f, m_SceneName));
            }
        }
    }

    private void UpdateCheckpointText()
    {
        if (m_CheckPointText != null)
        {
            m_CheckPointText.text = "Lap" + m_CheckPointsReached + "/" + m_TotalCheckPoints;
        }
    }
    private IEnumerator LoadSceneAfterDelay(float delay, string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
