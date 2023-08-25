using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneCharactor : MonoBehaviour
{
    [SerializeField,Header("�v���C���[�̃v���n�u")]
    private GameObject[] m_PlayerPrefabs;
    [SerializeField,Header("�X�|�[������|�C���g")]
    public Transform m_SpawnPosition;

    void Start()
    {
        // �I�����ꂽ�L�����N�^�[���(CharacterSelectData)���擾
        string selectedCharacter = CharacterSelectData.SelectedCharacter;

        // �I�����ꂽ�L�����N�^�[�ɉ����ăv���n�u��\��
        GameObject characterPrefab = GetCharacterPrefab(selectedCharacter);
        Instantiate(characterPrefab, m_SpawnPosition.position, Quaternion.identity);
    }
    //�L�����N�^�[�̖��O���󂯎��
    GameObject GetCharacterPrefab(string characterName)
    {
        //m_PlayerPrefabs�̗v�f�����ԂɃ��[�v
        foreach (GameObject prefab in m_PlayerPrefabs)
        {
            //�^�C�g���őI�������L�����̖��O�ƈ�v���邩�ǂ����`�F�b�N
            if (prefab.name == characterName)
            {
                //��v�����ꍇ�v���n�u��Ԃ�
                return prefab;
            }
        }
        //�������Null
        return null;
    }
}
