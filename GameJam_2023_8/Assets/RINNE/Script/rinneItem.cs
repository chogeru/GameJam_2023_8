using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rinneItem : zikken
{
    [Header("ƒAƒCƒeƒ€‰æ‘œ")]
    [SerializeField] private GameObject Sprite;
    
    public override void Initialize(Vector3 position, float speedModifier)
    {
        base.Initialize(position, speedModifier);

        Sprite?.SetActive(true);
    }
}
