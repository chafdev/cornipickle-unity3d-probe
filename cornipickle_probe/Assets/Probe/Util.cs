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
}
