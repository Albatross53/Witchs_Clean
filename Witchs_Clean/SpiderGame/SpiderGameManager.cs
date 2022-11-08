using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderGameManager : MonoBehaviour
{
    int _count = 0;
    public GameObject RestartMenu;
    public GameObject menu;
    public GameObject bag;

    public Slider timeBar;
    float spiderTime;

    public int spiderCount;
    public float spawnTime;
    public Transform[] spawPoint;
    public GameObject spider_1;
    public GameObject spider_2;
    public int killSpider = 0;

    public static SpiderGameManager _instance;

    //public GameObject next;
    public GameObject fail;
    public GameObject ready;
    public GameObject pos;
    public GameObject tutorialPannel;
    public bool isStart;

    void Start()
    {
        RestartMenu.SetActive(false);
        menu.SetActive(false);
        bag.SetActive(false);
        spiderTime = Glober.curTime;
        isStart = false;
        _instance = this;
        timeBar.maxValue = Glober.maxTime;
        
        if (Glober.m_nowStageCode == 21)
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
        for (int i = 0; i < Glober.maxSpiderCount; i++)
        {
            int y = Random.Range(0, spawPoint.Length);
            SpawnSpider(y, Glober.spiderLevel);
        }
    }
    void Update()
    {
        timeBar.value = spiderTime;
        if(isStart == true)
        {
            spiderTime -= Time.deltaTime;
            
            /*
            if (spiderTime >= spawnTime && spiderCount < Glober.maxSpiderCount)
            {
                int y = Random.Range(0, spawPoint.Length);
                SpawnSpider(y, Glober.spiderLevel);
            }
            */
        }

        if (bag.activeSelf == true || menu.activeSelf == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (spiderTime <= 0)
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
        else if (killSpider >= Glober.maxSpiderCount)
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
        killSpider = 0;
    }

    void RestartGame()
    {
        ScenManager.Instance.LoadScene("SpiderGame");
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
        spiderTime = Glober.time;
        RestartMenu.SetActive(false);
        Instantiate(ready, gameObject.transform.position, Quaternion.identity);
        */
    }

    void GameClear()
    {
        isStart = false;
        Glober.gameState = 0;
        Glober.gameValue = 0;
        Glober.curTime = spiderTime;
        GameDataManager.Instance.GameClear();
    }

    public void SpawnSpider(int ranNum, int levelNum)
    {
        spiderCount++;
        if (levelNum == 1)
        {
            Instantiate(spider_1, spawPoint[ranNum]);
        } else if (levelNum == 2)
        {
            int ran = Random.Range(0, 2);
            if(ran == 0)
            {
                Instantiate(spider_1, spawPoint[ranNum]);
            }else if(ran == 1)
            {
                Instantiate(spider_2, spawPoint[ranNum]);
            }
        }
    }

    public void TimeItem()
    {
        spiderTime += 20;
        if (spiderTime > Glober.maxTime)
        {
            spiderTime = Glober.maxTime;
        }
        bag.SetActive(false);
    }

}
