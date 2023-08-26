using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDestroy : MonoBehaviour
{
    [SerializeField, Header("アイテム所得時のエフェクト")]
    private GameObject m_ItemEffect;
    [SerializeField, Header("アイテム所得時のSE")]
    private AudioClip m_ItemGetSE;
    private float mVolume=1;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Car"))
        {
            //サウンドの再生
            AudioSource.PlayClipAtPoint(m_ItemGetSE,transform.position,mVolume);
            //パーティクルの複製
            Instantiate(m_ItemEffect.gameObject.transform);
            Destroy(gameObject);
        }
    }
}
