using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Liflab : MonoBehaviour
{

    public bool contain = false;
    private Button btn;
    bool clicked = false;
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => click());
    }

    private void click()
    {
        clicked = !clicked;
        Config.instance.showPopup(clicked);

    }
}
