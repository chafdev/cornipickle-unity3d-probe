using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public List<string> lstNameFile = new List<string>();

    public void clickMisalignedElements()
    {
        SceneManager.LoadScene(1);
       
    }

    public void clickOverlapping()
    {
        SceneManager.LoadScene(2);
    }

    public void clickOutside()
    {
        SceneManager.LoadScene(3);
    }

    public void clickButtonDoesntWork()
    {
        SceneManager.LoadScene(4);

    }

    public void GroupeItem()
    {
        SceneManager.LoadScene(5);
    }

    public void listTree()
    {
        SceneManager.LoadScene(6);
    }

    public void btnTouchSize()
    {
      
    }
}
