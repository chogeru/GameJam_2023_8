using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ItemPrefabs;
    [SerializeField]
    private Transform m_SpownPoint;
    private bool isItemSponInterVal=false;

    private float m_SpownTime=5;
    private float m_Time;

    [SerializeField]
    private Rigidbody rd;
    private float m_Power=15;
    private void Start()
    {
        rd = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(isItemSponInterVal==true)
        {
            m_Time += Time.deltaTime;
            if(m_Time > m_SpownTime )
            {
                m_Time = 0;
                isItemSponInterVal = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if  (other.CompareTag("Player")||other.CompareTag("Car"))
        {
            if (isItemSponInterVal==false)
            {
                isItemSponInterVal = true;
                //新しいアイテムをプレハブから生成する
                GameObject Item= Instantiate(m_ItemPrefabs, m_SpownPoint.position, Quaternion.identity);
                //リジットボディを所得
                Rigidbody rigidbody=Item.GetComponent<Rigidbody>();
                rigidbody.velocity = m_SpownPoint.forward * m_Power;
            }
        }
    }
}
