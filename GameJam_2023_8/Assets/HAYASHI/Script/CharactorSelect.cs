using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorSelect : MonoBehaviour
{
    //�^����ꂽ�L�����N�^�[����CharacterSelectData.SelectedCharacter�ɑ��
    public void SelectCharacter(string characterName)
    {
        CharacterSelectData.SelectedCharacter = characterName;
    }

}
