using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartMenu : MonoBehaviour
{
    
    public void RestartBtn()
    {
        GameDataManager.Instance.ResetAddValue();
        GameDataManager.Instance.GameStart();
        //GameObject.Find("Manager").SendMessage("RestartGame");
    }

    public void GiveupBtn()
    {
        GameDataManager.Instance.ResetAddValue();
        ScenManager.Instance.LoadScene("Main");
    }
}
