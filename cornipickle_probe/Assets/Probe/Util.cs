using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{

    //link https://gist.github.com/gauravmehla/583349e5127ec292bd95b371abe8e3e0
    // Calculate aspect ratio
    /// <summary>
    /// w/gcd,wh/gcd
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    static int gcd(int width, int height)
    {
        return (height == 0) ? width : gcd(height, width % height);
    }

    // Used to scale image according to aspect ratio
    Vector2 calculateAspectRatioFit(int srcWidth, int srcHeight, int maxWidth, int maxHeight)
    {
        var ratio = Mathf.Min(maxWidth / srcWidth, maxHeight / srcHeight);
        return new Vector2(srcWidth * ratio, srcHeight * ratio);
    }


    public static string getScreenOrientation()
    {

        return Screen.orientation.ToString();
    }

    public static Vector2 getDeviceWH()
    {

        return new Vector2(Screen.width, Screen.height);
    }
    public static Vector2 getCanvasWH(Canvas c)
    {
        RectTransform rectTr = c.gameObject.GetComponent<RectTransform>();
        float cWidth = rectTr.rect.width * rectTr.localScale.x;
        float cHeight = rectTr.rect.height * rectTr.localScale.y;
        return new Vector2(cWidth, cHeight);
    }
    /// <summary>
    /// The language the user's operating system is running in. Returned by Application.systemLanguage.
    /// </summary>
    /// <returns></returns>
    public string getLangueApp()
    {
        return Application.systemLanguage.ToString();
    }
  public  enum CornerRectangle{
        left,
        top,
        right,
        bottom


    }
  public  static float DisplayWorldCorners(RectTransform rt,CornerRectangle corner)
    {
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);

        switch (corner) { 
        case CornerRectangle.left:
                return v[0].x;
            case CornerRectangle.bottom:
                return v[1].y - rt.sizeDelta.y;
            case CornerRectangle.top:
             return   Screen.height - (v[3].y + rt.sizeDelta.y);// top





        }
        Debug.Log("World Corners");
        for (var i = 0; i < 4; i++)
        {
            Debug.Log("World Corner " + i + " : " + v[i]);
        }
        return 0;
    }
    internal static float getAbsoluteLeft(Component v)
    {
        return 1.0f;
    }

    internal static float getAbsoluteRight(Component v)
    {
        return 1.0f;
    }

    internal static float getAbsoluteTop(Component v)
    {
        return 1.0f;
    }

    internal static float getAbsoluteBottom(Component v)
    {
        return 1.0f;
    }

    internal static string getBackground(Component v)
    {
        return "rouge";
    }
}
