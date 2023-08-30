using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemBoxAnimeTion : MonoBehaviour
{
    [Header("アイテムゲット時のアニメーション用オブジェクト")]
    private GameObject m_ItemGetAnimeObj;
    [SerializeField,Header("UIの格納用")]
    private GameObject[] m_UIObjects; 
    [SerializeField,Header("タグ")]
    private string m_UiTag = "ItemUI"; 
    [SerializeField]
    private bool isItemGet;
    // UIをアクティブにする許可フラグ
    private bool isCanActivateUI = false;
    // UIをアクティブにするまでの待機時間
    private float m_ActivationDelay = 3f;
    // 待機タイマー
    private float m_ActivationTimer = 0f; 
    [SerializeField, Header("アイテム所得時のSE")]
    private GameObject m_ItemGetSE;
    [SerializeField, Header("アイテム確定時のSE")]
    private GameObject m_ItemSetSE;
    // Start is called before the first frame update
    void Start()
    {
        m_ItemGetAnimeObj = GameObject.Find("アイテムアニメーション");
        m_ItemGetAnimeObj.SetActive(false);
        m_ItemGetSE = GameObject.Find("ItemGetSE");
        m_ItemGetSE.SetActive(false);
        m_ItemSetSE = GameObject.Find("ItemSetSE");
        m_ItemSetSE.SetActive(false);
        m_UIObjects = GameObject.FindGameObjectsWithTag(m_UiTag);
      
        // 初期状態では全てのUIオブジェクトを非アクティブにする
        foreach (GameObject uiObject in m_UIObjects)
        {
            uiObject.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            m_ItemGetAnimeObj.SetActive(true);
            //UIアクティブ
            isCanActivateUI = true; 
            //アイテムゲット時のSEを再生
            m_ItemGetSE.SetActive(true);
            // 待機タイマーを初期化
            m_ActivationTimer = 0f;
        }
    }

    private void Update()
    {
        // UIがアクティブな場合
        if (isCanActivateUI)
        {
            // 待機タイマーを増加させる
            m_ActivationTimer += Time.deltaTime;

            // 待機タイマーが指定の時間を超えた場合、ランダムにUIオブジェクトをアクティブにする
            if (m_ActivationTimer >= m_ActivationDelay)
            {
                int randomIndex = Random.Range(0, m_UIObjects.Length);
                for (int i = 0; i < m_UIObjects.Length; i++)
                {
                    m_UIObjects[i].SetActive(i == randomIndex);
                }
                //アイテムセット時のSEも再生
                m_ItemSetSE.SetActive(true);
                //すでに再生されているアイテムゲット時のSEをストップ
                m_ItemGetSE.SetActive(false);
                // UIのアクティブを無効化し、待機タイマーをリセット
                isCanActivateUI = false;
                m_ActivationTimer = 0f;
            }
        }
    }
}
