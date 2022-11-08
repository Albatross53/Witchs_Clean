using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionManager : MonoBehaviour
{
    int _count = 0;
    public GameObject RestartMenu;
    public GameObject menu;
    public GameObject bag;

    public Slider timeBar;
    float PotionTime;

    public Image potionSlot;
    public GameObject answer;
    public GameObject select;
    public Sprite pinkPotion;
    public Sprite bluePotion;
    public Sprite greenPotion;

    List<GameObject> answerPotions = new List<GameObject>();
    List<GameObject> SelectPotions = new List<GameObject>();

    int[] m_potionAnswer = new int[Glober.maxPotionNum];
    int[] m_potionSelect = new int[Glober.maxPotionNum];

    int pressNum = 0;
    int answerNum = 0;

    //public GameObject next;
    public GameObject fail;
    public GameObject ready;
    public GameObject pos;
    public GameObject tutorialPannel;

    bool isStart;
    bool isItem;
    public static PotionManager _instance;

    void Start()
    {
        RestartMenu.SetActive(false);
        menu.SetActive(false);
        bag.SetActive(false);
        PotionTime = Glober.curTime;
        _instance = this;
        isStart = false;
        isItem = false;
        timeBar.maxValue = Glober.maxTime;
        if (Glober.m_nowStageCode == 11)
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
        for (int i = 0; i < Glober.maxPotionNum; i++)
        {
            int emp = Random.Range(0, 3);
            m_potionAnswer[i] = emp;
            Image A_potion = Instantiate(potionSlot, answer.transform);
            Image S_potion = Instantiate(potionSlot, select.transform);
        }
        foreach (Transform A_potion in answer.transform)
        {
            answerPotions.Add(A_potion.gameObject);
        }
        foreach (Transform S_potion in select.transform)
        {
            SelectPotions.Add(S_potion.gameObject);
        }
        for (int i = 0; i < Glober.maxPotionNum; i++)
        {
            SelectPotions[i].SetActive(false);
        }

        AnswerPanel();
    }

    void AnswerNum()
    {
        pressNum = 0;
        for (int i = 0; i < Glober.maxPotionNum; i++)
        {
            int emp = Random.Range(0, 3);
            m_potionAnswer[i] = emp;
            SelectPotions[i].SetActive(false);
        }
        AnswerPanel();
    }

    void Update()
    {
        timeBar.value = PotionTime;
        if(isStart == true)
        {
            PotionTime -= Time.deltaTime;
        }

        if (bag.activeSelf == true || menu.activeSelf == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (PotionTime <= 0)
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
        else if (answerNum >= Glober.PotionAnswerNum && pressNum >= Glober.maxPotionNum)
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
        pressNum = 0;
        answerNum = 0;
    }

    void RestartGame()
    {
        ScenManager.Instance.LoadScene("PotionGame");
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
        PotionTime = Glober.time;
        RestartMenu.SetActive(false);
        Instantiate(ready, gameObject.transform.position, Quaternion.identity);
        */
    }

    void AnswerPanel()
    {
        answerNum++;
        for (int i = 0; i < Glober.maxPotionNum; i++)
        {
            if(m_potionAnswer[i] == 0)
            {
                answerPotions[i].GetComponent<Image>().sprite = pinkPotion;
            }else if (m_potionAnswer[i] == 1)
            {
                answerPotions[i].GetComponent<Image>().sprite = greenPotion;
            }else if(m_potionAnswer[i] == 2)
            {
                answerPotions[i].GetComponent<Image>().sprite = bluePotion;
            }
        }
    }
    public void PinkPotion()
    {
        m_potionSelect[pressNum] = 0;
        if (m_potionAnswer[pressNum] == m_potionSelect[pressNum])
        {
            SelectPotions[pressNum].SetActive(true);
            SelectPotions[pressNum].GetComponent<Image>().sprite = pinkPotion;
            pressNum++;
            if(answerNum < Glober.PotionAnswerNum && pressNum >= Glober.maxPotionNum)
            {
                if (isItem)
                {
                    PotionItem();
                }
                else
                {
                    AnswerNum();
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }
    public void GreenPotion()
    {
        m_potionSelect[pressNum] = 1;
        if (m_potionAnswer[pressNum] == m_potionSelect[pressNum])
        {
            SelectPotions[pressNum].SetActive(true);
            SelectPotions[pressNum].GetComponent<Image>().sprite = greenPotion;
            pressNum++;
            if (answerNum < Glober.PotionAnswerNum && pressNum >= Glober.maxPotionNum)
            {
                if (isItem)
                {
                    PotionItem();
                }
                else
                {
                    AnswerNum();
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }

    public void BluePotion()
    {
        m_potionSelect[pressNum] = 2;
        if (m_potionAnswer[pressNum] == m_potionSelect[pressNum])
        {
            SelectPotions[pressNum].SetActive(true);
            SelectPotions[pressNum].GetComponent<Image>().sprite = bluePotion;
            pressNum++;
            if (answerNum < Glober.PotionAnswerNum && pressNum >= Glober.maxPotionNum)
            {
                if (isItem)
                {
                    PotionItem();
                }
                else
                {
                    AnswerNum();
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }

    void GameClear()
    {
        isStart = false;
        Glober.gameState = 0;
        Glober.gameValue = 2;
        Glober.curTime = PotionTime;
        GameDataManager.Instance.GameClear();
    }



    public void PotionItem()
    {
        pressNum = 0;
        int emp = Random.Range(0, 3);
        for (int i = pressNum; i < Glober.maxPotionNum; i++)
        {
            m_potionAnswer[i] = emp;
            SelectPotions[i].SetActive(false);
        }
        isItem = true;
        AnswerPanel();
        bag.SetActive(false);
    }
    public void TimeItem()
    {
        PotionTime += 20;
        if (PotionTime > Glober.maxTime)
        {
            PotionTime = Glober.maxTime;
        }
        bag.SetActive(false);
    }

}
