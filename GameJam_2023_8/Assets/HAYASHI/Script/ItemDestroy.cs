using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDestroy : MonoBehaviour
{
    [SerializeField, Header("�A�C�e���������̃G�t�F�N�g")]
    private GameObject m_ItemEffect;
    [SerializeField, Header("�A�C�e����������SE")]
    private AudioClip m_ItemGetSE;
    private float mVolume=1;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Car"))
        {
            //�T�E���h�̍Đ�
            AudioSource.PlayClipAtPoint(m_ItemGetSE,transform.position,mVolume);
            //�p�[�e�B�N���̕���
            Instantiate(m_ItemEffect.gameObject.transform);
            Destroy(gameObject);
        }
    }
}
