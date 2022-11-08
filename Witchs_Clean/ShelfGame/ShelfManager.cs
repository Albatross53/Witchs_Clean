using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShelfManager : MonoBehaviour
{
    int _count = 0;

    public GameObject RestartMenu;
    public GameObject menu;
    public GameObject bag;

    public Slider timeBar;
    float shelfTime;

    public GameObject Shelf;
    public Image bookSlot;

    public Sprite rbIcon;
    public Sprite bbIcon;
    public Sprite redBook;
    public Sprite blueBook;

    int[] m_ShelfAnswer = new int[Glober.maxShelfNum];
    int[] m_ShelfSelect = new int[Glober.maxShelfNum];
    int pressNum = 0;
    List<GameObject> books = new List<GameObject>();

    //public GameObject next;
    public GameObject fail;
    public GameObject ready;
    public GameObject pos;
    public GameObject tutorialPannel;

    bool isStart;
    public static ShelfManager _instance;

    void Start()
    {
        RestartMenu.SetActive(false);
        menu.SetActive(false);
        bag.SetActive(false);
        shelfTime = Glober.curTime;
        _instance = this;
        isStart = false;
        timeBar.maxValue = Glober.maxTime; 
        if (Glober.m_nowStageCode == 12)
        {
            Tutorial();
        }
        else
        {
            tutorialPannel.SetActive(false);
            Instantiate(ready, pos.transform);
        }
    }

    void Tutorial()
    {
        tutorialPannel.SetActive(true);
    }

    public void TurotrialEnd()
    {
        tutorialPannel.SetActive(false);
        Instantiate(ready, pos.transform);
    }

    public void startGame()
    {
        isStart = true;
        for (int i = 0; i < Glober.maxShelfNum; i++)
        {
            int emp = Random.Range(0, 2);
            m_ShelfAnswer[i] = emp;
            Image book = Instantiate(bookSlot, Shelf.transform);
        }
        foreach (Transform book in Shelf.transform)
        {
            books.Add(book.gameObject);
        }
        for (int i = 0; i < Glober.maxShelfNum; i++)
        {
            books[i].SetActive(false);
        }
        ShelfIcon();
    }
    void Update()
    {
        timeBar.value = shelfTime;
        if(isStart == true)
        {
            shelfTime -= Time.deltaTime;
        }

        if (bag.activeSelf == true || menu.activeSelf == true || RestartMenu.activeSelf == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (shelfTime <= 0)
        {
            //TimeOut
            ++_count;
            if (_count > 1)
            {
                return;
            }
            isStart = false;
            Instantiate(fail, pos.transform);
        }
        else if (pressNum == Glober.maxShelfNum)
        {
            //GameClear            
            ++_count;
            if (_count > 1)
            {
                return;
            }
            GameClear();
            //Instantiate(next, pos.transform);
        }

    }

    public void Fail()
    {
        pressNum = 0;
        RestartMenu.SetActive(true);
    }

    void RestartGame()
    {
        ScenManager.Instance.LoadScene("ShelfGame");
        int m_nowStageCode = Glober.m_nowStageCode;
        if (m_nowStageCode >= 11 && m_nowStageCode <= 13)
        {
            PlayerValueManager.Instance.IsNowHealth -= 5;
        }
        else
        {
            PlayerValueManager.Instance.IsNowHealth -= 10;
        }
        /*
        shelfTime = Glober.time;
        RestartMenu.SetActive(false);
        Instantiate(ready, pos.transform);
        */
    }

    void GameClear()
    {
        isStart = false;
        Glober.gameState = 0;
        Glober.gameValue = 3;
        Glober.curTime = shelfTime;
        GameDataManager.Instance.GameClear();
    }

    void ShelfIcon()
    {
        //bookicon
        if (pressNum >0 && pressNum % 11 == 0)
        {
            var shelfPos = Shelf.transform.localPosition;
            Shelf.transform.localPosition = new Vector3(shelfPos.x - 1100, shelfPos.y, shelfPos.z);
        }
        if (pressNum < Glober.maxShelfNum)
        {
            books[pressNum].SetActive(true);
            if (m_ShelfAnswer[pressNum] == 0)
            {
                books[pressNum].GetComponent<Image>().sprite = rbIcon;
            }
            else if (m_ShelfAnswer[pressNum] == 1)
            {
                books[pressNum].GetComponent<Image>().sprite = bbIcon;
            }
        }
    }
    void ShelfBook()
    {
        //book
        if (m_ShelfAnswer[pressNum] == 0)
        {
            books[pressNum].GetComponent<Image>().sprite = redBook;
        }
        else if (m_ShelfAnswer[pressNum] == 1)
        {
            books[pressNum].GetComponent<Image>().sprite = blueBook;
        }
    }
    //redBook
    public void RedBook()
    {
        if (pressNum < Glober.maxShelfNum)
        {
            m_ShelfSelect[pressNum] = 0;
            if (m_ShelfAnswer[pressNum] == m_ShelfSelect[pressNum])
            {
                ShelfBook();
                pressNum++;
                ShelfIcon();
            }
            else
            {
                return;
            }
        }
    }
    //BlueBook
    public void BlueBook()
    {
        if (pressNum < Glober.maxShelfNum)
        {
            m_ShelfSelect[pressNum] = 1;
            if (m_ShelfAnswer[pressNum] == m_ShelfSelect[pressNum])
            {
                ShelfBook();
                pressNum++;
                ShelfIcon();
            }
        }
    }
    public void ShelfItem()
    {
        int emp = m_ShelfAnswer[pressNum];

        for (int i = pressNum; i < Glober.maxShelfNum; i++)
        {
            m_ShelfAnswer[i] = emp;
        }
        bag.SetActive(false);
    }
    public void TimeItem()
    {
        shelfTime += 20;
        if (shelfTime > Glober.maxTime)
        {
            shelfTime = Glober.maxTime;
        }
        bag.SetActive(false);
    }

}
