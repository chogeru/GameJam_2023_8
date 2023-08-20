using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [Header("アイテムプレハブ")]
    [SerializeField]
    private Item[] itemPrefabArray;
    [SerializeField]
    private int[] itemSpawnWeightArray;
    private int[] itemSpawnRandomIndexList;

    void Start()
    {
        //重みリストをインデックスのリストに変換する
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
                //生成位置を決める
                var newPosition = new Vector3();

                //アイテムを生成
                var targetPrefab = RandomPickItemPrefab();
                var newObject = Instantiate(targetPrefab);
                //初期化する
                //newObject.Initialize(newPosition)
            }
               
        }
    }
    /// <summary>
    /// 重みに基づいてランダムにアイテムを選択
    /// </summary>
    private Item RandomPickItemPrefab()
    {
        var index = itemSpawnRandomIndexList[Random.Range(0, itemSpawnRandomIndexList.Length)];
        var prefab = itemPrefabArray[index];
        return prefab;
    }

}
