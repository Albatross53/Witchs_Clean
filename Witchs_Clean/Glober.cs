using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glober
{
    public static float maxTime = 60;
    public static float curTime = 60;

    public static int maxSpiderCount = 10;
    public static int spiderLevel = 2;
    
    public static int maxStuffNum = 20;

    public static int maxShelfNum = 40;

    public static int maxPotionNum = 4;
    public static int PotionAnswerNum = 10;

    public static int m_nowStageCode;

    public static int stage = 0;
    // 0: clear, 1: timeout
    public static int gameState = 0;
    // 0: spider, 1: potion, 2: stuff, 3: shelf
    public static int gameValue = 0; 
}