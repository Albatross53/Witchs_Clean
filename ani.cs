using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ani : MonoBehaviour
{
    public void ready()
    {
        GameObject.Find("Manager").SendMessage("startGame");
        Destroy(gameObject);
    }
    public void clear()
    {
        GameObject.Find("Manager").SendMessage("GameClear");
        Destroy(gameObject);
    }
    public void fail()
    {
        GameObject.Find("Manager").SendMessage("Fail");
        Destroy(gameObject);
    }
}
