using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPoint : MonoBehaviour
{
    private bool isSelected = false;
    public int stt;
    public void SetData()
    {
        isSelected = false;
        GetComponent<PolygonCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = false;
        SetUnCollected();
    }

    public void SetCollected()
    {
        if(isSelected) return;
        isSelected = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<PolygonCollider2D>().enabled = false;
    }
    public void SetUnCollected()
    {
        isSelected = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = true;
    }
}
