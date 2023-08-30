using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSwitcher : MonoBehaviour
{
    public Sprite jesika;
    public Sprite sara;
    public Sprite koberu;
    public Sprite anasutasia;
    public Sprite shery;
    public Sprite suzuka;

    private Image image;

    [SerializeField]
    RankChecker rankchecker;
    // Start is called before the first frame update
    void Start()
    {


        ////オブジェクトを名前で探す
        //GameObject resultObj = GameObject.Find("Image_1st");
        ////変数「test」を参照しDebug.Logに出力
        //Debug.Log(resultObj.GetComponent<GamePlaying>().test);

        for (int i = 0; i < 3; i++)
        {
            var name = rankchecker.GetRankText().Split(" / ")[i];
            GameObject resultObj = 
                (i == 1) ? GameObject.Find("Image_1st")
                : (i == 2) ? GameObject.Find("Image_2nd")
                : GameObject.Find("Image_3rd");

            Glo

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
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
