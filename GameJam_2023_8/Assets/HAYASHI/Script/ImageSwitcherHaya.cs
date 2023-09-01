using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ImageSwitcherHaya : MonoBehaviour
{
    public ParticleSystem ps;
    public Sprite jesika;
    public Sprite sara;
    public Sprite koberu;
    public Sprite anasutasia;
    public Sprite shery;
    public Sprite suzuka;
    public string m_SceneName = "";

    private Image image;

    // Start is called before the first frame update
    void Start()
    {
       // ps?.Play();

        Debug.Log("アプリケーションが開始されました。");
        //オブジェクトを名前で探す
        var rankChecerObj = GameObject.Find("RankChecker");
        var rankChecer = rankChecerObj.GetComponent<RankCheckerHaya>();
        var rankText = rankChecer.GetRankText(); // "ジェシカ / サラ / コベル";
        var names = rankText.Split(" / ");
        // スプライトを設定するオブジェクトの名前を定義
        string[] resultObjectNames = { "Image_1st", "Image_2nd", "Image_3rd" };

        // names 配列に格納された名前ごとにスプライトを設定
        for (int i = 0; i < Mathf.Min(names.Length, 3); i++)
        {
            var name = names[i];
            var resultObj = GameObject.Find(resultObjectNames[i]);
            /*
            for (int i = 0; i < 3; i++)
            {
                var name = names[i];
                GameObject resultObj =
                    (i == 1) ? GameObject.Find("Image_1st")
                    : (i == 2) ? GameObject.Find("Image_2nd")
                    : GameObject.Find("Image_3rd");
                */
            switch (name)
            {
                case "ジェシカ":
                case "Playerジェシカ":
                    resultObj.GetComponent<Image>().sprite = jesika;
                    break;
                case "サラ":
                case "Playerサラ":
                    resultObj.GetComponent<Image>().sprite = sara;
                    break;
                case "コベル":
                case "Playerコベル":
                    resultObj.GetComponent<Image>().sprite = koberu;
                    break;
                case "アナスタシア":
                case "Playerアナスタシア":
                    resultObj.GetComponent<Image>().sprite = anasutasia;
                    break;
                case "シェリー":
                case "Playerシェリー":
                    resultObj.GetComponent<Image>().sprite = shery;
                    break;
                case "スズカ":
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
