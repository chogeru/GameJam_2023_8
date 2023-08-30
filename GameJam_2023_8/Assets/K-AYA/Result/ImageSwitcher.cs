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


        ////�I�u�W�F�N�g�𖼑O�ŒT��
        //GameObject resultObj = GameObject.Find("Image_1st");
        ////�ϐ��utest�v���Q�Ƃ�Debug.Log�ɏo��
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
                case "�W�F�V�J":
                    resultObj.GetComponent<Image>().sprite = jesika;
                    break;
                case "�T��":
                    resultObj.GetComponent<Image>().sprite = sara;
                    break;
                case "�R�x��":
                    resultObj.GetComponent<Image>().sprite = koberu;
                    break;
                case "�A�i�X�^�V�A":
                    resultObj.GetComponent<Image>().sprite = anasutasia;
                    break;
                case "�V�F���[":
                    resultObj.GetComponent<Image>().sprite = shery;
                    break;
                case "�X�Y�J":
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
