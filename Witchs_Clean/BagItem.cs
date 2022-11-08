using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BagItem : MonoBehaviour
{
    public ItemData item;
    int code;
    Button btn;
    Text itemCount;

    void Start()
    {
        code = item.m_itemCode;
        this.transform.Find("ItemSpite").gameObject.GetComponent<Image>().sprite = item.m_itemImage;
        this.transform.Find("ItemName").gameObject.GetComponent<Text>().text = item.m_itemName;
        itemCount = this.transform.Find("ItemCount").gameObject.GetComponent<Text>();
        btn = this.transform.Find("Usebtn").gameObject.GetComponent<Button>();
        btn.onClick.AddListener(use);
    }

    void Update()
    {
        itemCount.text = GameDataManager.Instance.m_itemAmountDic[code].ToString();
        if (GameDataManager.Instance.m_itemAmountDic[code] <= 0)
        {
            GameDataManager.Instance.m_itemAmountDic[code] = 0;
            btn.interactable = false;
        }
        else
        {
            btn.interactable = true;
        }
    }

    public void use()
    {
        switch (code)
        {
            case 0:
                if(SceneManager.GetActiveScene().name == "SpiderGame")
                {
                    GameDataManager.Instance.m_itemAmountDic[code] -= 1;
                    SlingShot._instance.SlingshotItme();
                }
                else
                {
                    return;
                }
                break;
            case 1:
                if (SceneManager.GetActiveScene().name == "ShelfGame")
                {
                    GameDataManager.Instance.m_itemAmountDic[code] -= 1;
                    ShelfManager._instance.ShelfItem();
                }
                else
                {
                    return;
                }
                break;
            case 2:
                if(SceneManager.GetActiveScene().name == "PotionGame")
                {
                    GameDataManager.Instance.m_itemAmountDic[code] -= 1;
                    PotionManager._instance.PotionItem();
                }
                else
                {
                    return;
                }
                break;
            case 3:
                if(SceneManager.GetActiveScene().name == "StuffGame")
                {
                    GameDataManager.Instance.m_itemAmountDic[code] -= 1;
                    StuffManager._instance.stuffItem();
                }
                else
                {
                    btn.interactable = false;
                    return;
                }
                break;
            case 4:
                GameDataManager.Instance.m_itemAmountDic[code] -= 1;
                GameObject.Find("Manager").SendMessage("TimeItem");
                break;
        }
    }
}
