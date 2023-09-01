using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSwitcherHaya : MonoBehaviour
{
    public Sprite jesika;
    public Sprite sara;
    public Sprite koberu;
    public Sprite anasutasia;
    public Sprite shery;
    public Sprite suzuka;

    private Image image;
     
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("アプリケーションが開始されました。");
        //オブジェクトを名前で探す
        var rankChecerObj = GameObject.Find("RankChecker");
        var rankChecer = rankChecerObj.GetComponent<RankCheckerHaya>();
        var rankText = rankChecer.GetRankText(); // "ジェシカ / サラ / コベル";
        var names = rankText.Split(" / ");

        for (int i = 0; i < 3; i++)
        {
            var name = names[i];
            GameObject resultObj =
                (i == 1) ? GameObject.Find("Image_1st")
                : (i == 2) ? GameObject.Find("Image_2nd")
                : GameObject.Find("Image_3rd");

            switch (name)
            {
                case "ジェシカ":
                    resultObj.GetComponent<Image>().sprite = jesika;
                    break;
                case "サラ":
                    resultObj.GetComponent<Image>().sprite = sara;
                    break;
                case "コベル":
                    resultObj.GetComponent<Image>().sprite = koberu;
                    break;
                case "アナスタシア":
                    resultObj.GetComponent<Image>().sprite = anasutasia;
                    break;
                case "シェリー":
                    resultObj.GetComponent<Image>().sprite = shery;
                    break;
                case "スズカ":
                    resultObj.GetComponent<Image>().sprite = suzuka;
                    break;
                case "Playerジェシカ":
                    resultObj.GetComponent <Image>().sprite = jesika;
                    break;
                case "Playerサラ":
                    resultObj.GetComponent<Image>().sprite = sara;
                    break;
                case "Playerコベル":
                    resultObj.GetComponent<Image>().sprite = koberu;
                    break;
                case "Playerアナスタシア":
                    resultObj.GetComponent<Image>().sprite = anasutasia;
                    break;
                case "Playerシェリー":
                    resultObj.GetComponent<Image>().sprite = shery;
                    break;
                case "Playerスズカ":
                    resultObj.GetComponent<Image>().sprite = suzuka;
                    break;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
