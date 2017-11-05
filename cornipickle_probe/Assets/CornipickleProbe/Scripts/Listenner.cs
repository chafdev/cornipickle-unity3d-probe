using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Listenner : MonoBehaviour {

  Probe.MotionEvent evt;

    public Probe.MotionEvent Evt
    {
        get
        {
            Probe.MotionEvent evt1 = evt;
            evt = Probe.MotionEvent.non;
            return evt1;
        }

      
    }

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        
    }
    //Do this when the mouse click on this selectable UI object is released.
    public void OnPointerUp(PointerEventData eventData)
    {
        evt = Probe.MotionEvent.up;
        Debug.Log("The mouse click was released");
    }
    void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
        evt = Probe.MotionEvent.down;

    }
}