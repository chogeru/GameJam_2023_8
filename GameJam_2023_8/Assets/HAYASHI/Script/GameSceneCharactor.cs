using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneCharactor : MonoBehaviour
{
    [SerializeField,Header("プレイヤーのプレハブ")]
    private GameObject[] m_PlayerPrefabs;
    [SerializeField,Header("スポーンするポイント")]
    public Transform m_SpawnPosition;

    void Start()
    {
        // 選択されたキャラクター情報(CharacterSelectData)を取得
        string selectedCharacter = CharacterSelectData.SelectedCharacter;

        // 選択されたキャラクターに応じてプレハブを表示
        GameObject characterPrefab = GetCharacterPrefab(selectedCharacter);
        Instantiate(characterPrefab, m_SpawnPosition.position, Quaternion.identity);
    }
    //キャラクターの名前を受け取る
    GameObject GetCharacterPrefab(string characterName)
    {
        //m_PlayerPrefabsの要素を順番にループ
        foreach (GameObject prefab in m_PlayerPrefabs)
        {
            //タイトルで選択したキャラの名前と一致するかどうかチェック
            if (prefab.name == characterName)
            {
                //一致した場合プレハブを返す
                return prefab;
            }
        }
        //無ければNull
        return null;
    }
}
