using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StuffManager : MonoBehaviour
{
    int _count = 0;

    /// <summary>
    /// ui
    /// </summary>
    public GameObject RestartMenu;
    public GameObject menu;
    public GameObject bag;
    public Slider timeBar;
    float StuffTime;

    /// <summary>
    /// 재료
    /// </summary>
    public GameObject frog;
    public GameObject rabbit;
    public GameObject teeth;

    int[] spawnStuff = new int[Glober.maxStuffNum];
    public int curNum;
    bool isStart;
    public Transform spawn;

    //public GameObject next;
    public GameObject fail;
    public GameObject ready;
    public GameObject pos;
    public GameObject tutorialPannel;

    public static StuffManager _instance;

    void Start()
    {
        RestartMenu.SetActive(false);
        menu.SetActive(false);
        bag.SetActive(false);
        StuffTime = Glober.curTime;
        _instance = this;
        isStart = false;
        timeBar.maxValue = Glober.maxTime;
        if (Glober.m_nowStageCode == 13)
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
        curNum = 0;
        for (int i = 0; i < Glober.maxStuffNum; i++)
        {
            int emp = Random.Range(0, 3);
            spawnStuff[i] = emp;
        }
        CreateStuff();
    }
    void Update()
    {
        timeBar.value = StuffTime;
        if (isStart == true)
        {
            StuffTime -= Time.deltaTime;
        }


        if (bag.activeSelf == true || menu.activeSelf == true)
        {
            Time.timeScale = 0;
        }else
        {
            Time.timeScale = 1;
        }

        if (StuffTime <= 0)
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
        else if (curNum >= Glober.maxStuffNum)
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
        RestartMenu.SetActive(true);
    }

    void RestartGame()
    {
        ScenManager.Instance.LoadScene("StuffGame");
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
        StuffTime = Glober.time;
        RestartMenu.SetActive(false);
        Instantiate(ready, gameObject.transform.position, Quaternion.identity);
        */
    }

    void GameClear()
    {
        isStart = false;
        Glober.gameState = 0;
        Glober.gameValue = 2;
        Glober.curTime = StuffTime;
        GameDataManager.Instance.GameClear();
    }

    /// <summary>
    /// 재료 생성
    /// </summary>
    public void CreateStuff()
    {
        if (curNum < Glober.maxStuffNum)
        {
            if (spawnStuff[curNum] == 0)
            {
                Instantiate(teeth, spawn);
            }
            else if (spawnStuff[curNum] == 1)
            {
                Instantiate(frog, spawn);
            }
            else if (spawnStuff[curNum] == 2)
            {
                Instantiate(rabbit, spawn);
            }
        }
    }

    /// <summary>
    /// 재료 아이템 사용
    /// </summary>
    public void stuffItem()
    {
        int emp = spawnStuff[curNum];
        for (int i = curNum; i < Glober.maxStuffNum; i++)
        {
            spawnStuff[i] = emp;
        }
        bag.SetActive(false);
    }

    public void TimeItem()
    {
        StuffTime += 20;
        if(StuffTime > Glober.maxTime)
        {
            StuffTime = Glober.maxTime;
        }
        bag.SetActive(false);
    }
}
