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

    public static float getApsectRatio()
    {

        return Screen.width / Screen.height;
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
    public static Vector2 GetScreenDimen()
    {

        return new Vector2(Screen.width, Screen.height);
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
    public static string getLangueApp()
    {
        return Application.systemLanguage.ToString();
    }
    public enum CornerRectangle
    {
        left,
        top,
        right,
        bottom


    }

    public static Vector2 getWH(RectTransform rt)
    {
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        Rect newRect = new Rect(v[0], v[2] - v[0]);
        return newRect.size;
    }
    /// <summary>
    ///      Vector3[] corners = new Vector3[4];
   // rt.GetWorldCorners(corners);
    //        Rect newRect = new Rect(corners[0], corners[2] - corners[0]);
    /// </summary>
    /// <param name="rt"></param>
    /// <param name="corner"></param>
    /// <returns></returns>
    public static int DisplayWorldCorners(RectTransform rt, CornerRectangle corner)
    {
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        Rect newRect = new Rect(v[0], v[2] - v[0]);
        Debug.Log("rct" + newRect);
        switch (corner)
        {
            case CornerRectangle.left:
                return (int)newRect.position.x;
           
            case CornerRectangle.bottom:
                return (int)newRect.position.y;// top
            case CornerRectangle.top:
                return (int)(Screen.height- (newRect.position.y + newRect.size.y));
            case CornerRectangle.right:
                return (int)(Screen.width - (newRect.position.x+ newRect.size.x));
                
          /*      case CornerRectangle.left:
                    return v[0].x + rt.sizeDelta.x;
                case CornerRectangle.bottom:
                    return v[1].y - rt.sizeDelta.y;
                case CornerRectangle.top:
                    return Screen.height - (v[3].y + rt.sizeDelta.y);// top
                case CornerRectangle.right:
                    return v[2].x + rt.sizeDelta.x;*/

        }

        return 0;
    }
 
    internal static float getAbsoluteLeft(Component v)
    {
        if (v is RectTransform)

            return DisplayWorldCorners((RectTransform)v, CornerRectangle.left);
        return 0.1f;
    }

    internal static float getAbsoluteRight(Component v)
    {
        if (v is RectTransform)

            return DisplayWorldCorners((RectTransform)v, CornerRectangle.right);
        return 0.1f;

    }

    internal static float getAbsoluteTop(Component v)
    {
        if (v is RectTransform)

            return DisplayWorldCorners((RectTransform)v, CornerRectangle.top);
        return 0.1f;

    }

    internal static float getAbsoluteBottom(Component v)
    {
        if (v is RectTransform)

            return DisplayWorldCorners((RectTransform)v, CornerRectangle.bottom);
        return 0.1f;

    }

    internal static string getBackground(Component v)
    {
        return "";
    }
}
