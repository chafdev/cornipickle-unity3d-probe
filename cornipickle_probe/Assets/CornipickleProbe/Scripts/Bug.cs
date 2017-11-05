using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bug : MonoBehaviour
{

    public Vector2 pos;
    Vector2 posAnc;
    public GameObject g;
    RectTransform rect;
    public string key = "";
    void OnEnable()
    {

        rect = GetComponent<RectTransform>();
        posAnc = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y);
        Config.OnKeyForBug += setBug;
        Config.OnKeyForReset += setReset;
    }
    public void setBug()
    {
        if (key == "")
            rect.anchoredPosition = pos;
        else if (key == "list")
        {
            g.GetComponent<Text>().text = "Element 1";

        }
    }
    public void setReset()
    {
        if (key == "")
            rect.anchoredPosition = posAnc;
        else if (key == "list")
        {
            g.GetComponent<Text>().text = "Element";


        }
    }

    void OnDisable()
    {
        Config.OnKeyForBug -= setBug;
        Config.OnKeyForReset -= setReset;
    }
}
