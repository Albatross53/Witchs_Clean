using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public int spiderHealth;
    float amountomove = 6;
    float startY;
    int direecton;
    float speed;

    Animator m_Animator;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        startY = gameObject.transform.position.y;
        direecton = 0;
        speed = Random.Range(1, 4);
    }

    void Update()
    {
        if (SpiderGameManager._instance.isStart)
        {
            if (gameObject.transform.position.y > startY - amountomove && direecton == 0)
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime);
            }
            else if (gameObject.transform.position.y <= startY - amountomove && direecton == 0)
            {
                direecton = 1;
            }
            else if (gameObject.transform.position.y < startY && direecton == 1)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
            else if (gameObject.transform.position.y >= startY && direecton == 1)
            {
                //m_Animator.SetBool("hit", false);
                direecton = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Ball")
        {
            m_Animator.SetBool("hit", true);
            spiderHealth--;
            if (spiderHealth <= 0)
            {
                Destroy(gameObject);
                SpiderGameManager._instance.spiderCount--;
                SpiderGameManager._instance.killSpider++;
                Debug.Log(SpiderGameManager._instance.killSpider);
            }
        }
    }
}
