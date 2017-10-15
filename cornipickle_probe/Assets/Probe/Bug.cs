using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{

    public Vector2 pos;
     Vector2 posAnc;
    RectTransform rect;
    void OnEnable()
    {

        rect = GetComponent<RectTransform>();
        posAnc = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y);
        Config.OnKeyForBug += setBug;
        Config.OnKeyForReset += setReset;
    }
    public void setBug()
    {
        rect.anchoredPosition = pos;

    }
    public void setReset()
    {
        rect.anchoredPosition = posAnc;

    }

    void OnDisable()
    {
        Config.OnKeyForBug -= setBug;
        Config.OnKeyForReset -= setReset;
    }
}
