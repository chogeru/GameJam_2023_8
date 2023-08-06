using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankViewer : MonoBehaviour
{
    [SerializeField]
    RankChecker rankchecker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {;
        this.GetComponent<TextMeshProUGUI>().text = rankchecker.GetRankText();
    }
}
