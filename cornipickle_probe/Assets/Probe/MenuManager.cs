using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public List<string> lstNameFile = new List<string>();

    public void clickMisalignedElements()
    {
        Config.instance.loadProperties(lstNameFile[0]);
    }

    public void clickOverlapping()
    {
        Config.instance.loadProperties(lstNameFile[0]);
    }

    public void clickOutside()
    {
        Config.instance.loadProperties(lstNameFile[0]);
    }

    public void clickButtonDoesntWork()
    {
        Config.instance.loadProperties(lstNameFile[0]);

    }

    public void GroupeItem()
    {
        Config.instance.loadProperties(lstNameFile[0]);
    }

    public void listTree()
    {
        Config.instance.loadProperties(lstNameFile[0]);
    }

    public void btnTouchSize()
    {
        Config.instance.loadProperties(lstNameFile[0]);
    }
}
