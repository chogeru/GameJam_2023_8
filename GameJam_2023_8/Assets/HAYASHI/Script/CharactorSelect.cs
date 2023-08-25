using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorSelect : MonoBehaviour
{
    //与えられたキャラクター名をCharacterSelectData.SelectedCharacterに代入
    public void SelectCharacter(string characterName)
    {
        CharacterSelectData.SelectedCharacter = characterName;
    }

}
