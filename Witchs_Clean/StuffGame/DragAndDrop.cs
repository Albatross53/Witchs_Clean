using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool selected;

    void Update()
    {
        if (selected == true)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(cursorPos.x, cursorPos.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            selected = false;
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            selected = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (StuffManager._instance.curNum < Glober.maxStuffNum)
        {
            if (collision.gameObject.tag == this.tag)
            {
            StuffManager._instance.curNum++;
            StuffManager._instance.CreateStuff();
            Destroy(gameObject);
            }
        }
    }
}
