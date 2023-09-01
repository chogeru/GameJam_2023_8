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
        Debug.Log("�A�v���P�[�V�������J�n����܂����B");
        //�I�u�W�F�N�g�𖼑O�ŒT��
        var rankChecerObj = GameObject.Find("RankChecker");
        var rankChecer = rankChecerObj.GetComponent<RankCheckerHaya>();
        var rankText = rankChecer.GetRankText(); // "�W�F�V�J / �T�� / �R�x��";
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
                case "Player�W�F�V�J":
                    resultObj.GetComponent <Image>().sprite = jesika;
                    break;
                case "Player�T��":
                    resultObj.GetComponent<Image>().sprite = sara;
                    break;
                case "Player�R�x��":
                    resultObj.GetComponent<Image>().sprite = koberu;
                    break;
                case "Player�A�i�X�^�V�A":
                    resultObj.GetComponent<Image>().sprite = anasutasia;
                    break;
                case "Player�V�F���[":
                    resultObj.GetComponent<Image>().sprite = shery;
                    break;
                case "Player�X�Y�J":
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
