using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [Header("�A�C�e���v���n�u")]
    [SerializeField]
    private Item[] itemPrefabArray;
    [SerializeField]
    private int[] itemSpawnWeightArray;
    private int[] itemSpawnRandomIndexList;

    void Start()
    {
        //�d�݃��X�g���C���f�b�N�X�̃��X�g�ɕϊ�����
        itemSpawnRandomIndexList = itemSpawnWeightArray
            .SelectMany((w, i) => Enumerable.Repeat(i, w))
            .ToArray();
        StartCoroutine(DoLoop());
    }

    private IEnumerator DoLoop()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.T))
            {
                //�����ʒu�����߂�
                var newPosition = new Vector3();

                //�A�C�e���𐶐�
                var targetPrefab = RandomPickItemPrefab();
                var newObject = Instantiate(targetPrefab);
                //����������
                //newObject.Initialize(newPosition)
            }
               
        }
    }
    /// <summary>
    /// �d�݂Ɋ�Â��ă����_���ɃA�C�e����I��
    /// </summary>
    private Item RandomPickItemPrefab()
    {
        var index = itemSpawnRandomIndexList[Random.Range(0, itemSpawnRandomIndexList.Length)];
        var prefab = itemPrefabArray[index];
        return prefab;
    }

}
