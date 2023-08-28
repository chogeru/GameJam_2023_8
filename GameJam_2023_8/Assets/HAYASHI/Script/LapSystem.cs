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
    private void Start()
    {
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
                new WaitForSeconds(5f);
                SceneManager.LoadScene(m_SceneName);
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
}
