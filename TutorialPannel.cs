using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPannel : MonoBehaviour
{
    Button goBtn;
    Button nextBtn;
    Button prevBtn;

    public Sprite[] tutorialImage;
    public Sprite[] textlImage;

    int pageNum = 0;
    public int lastPage;
    void Start()
    {
        goBtn = this.transform.Find("go").gameObject.GetComponent<Button>();
        nextBtn = this.transform.Find("next").gameObject.GetComponent<Button>();
        prevBtn = this.transform.Find("prev").gameObject.GetComponent<Button>();

    }

    void Update()
    {
        if (pageNum <= 0)
        {
            prevBtn.interactable = false;
            nextBtn.interactable = true;
            goBtn.interactable = false;
        }
        else if (pageNum >= lastPage)
        {
            prevBtn.interactable = true;
            nextBtn.interactable = false;
            goBtn.interactable = true;
        }
        else
        {
            prevBtn.interactable = true;
            nextBtn.interactable = true;
            goBtn.interactable = false;
        }
        this.transform.Find("tutorialImage").gameObject.GetComponent<Image>().sprite = tutorialImage[pageNum];
        this.transform.Find("textImage").gameObject.GetComponent<Image>().sprite = textlImage[pageNum];
    }

    public void next()
    {
        pageNum++;
        if(pageNum > lastPage)
        {
            pageNum = lastPage;
        }
    }
    public void prev()
    {
        pageNum--;
        if (pageNum < 0)
        {
            pageNum = 0;
        }
    }

    public void start()
    {
        GameObject.Find("Manager").SendMessage("TurotrialEnd");
    }
}
