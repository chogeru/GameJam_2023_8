using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zikken : MonoBehaviour
{
    /// <summary>
    ///生成直後に呼び出されます
    /// </summary>
    public virtual void Initialize(Vector3 position,float speedModifier)
    {
        transform.position = position; 
    }
}


