using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        rt = GetComponent<RectTransform>();
        Debug.Log(Screen.height);
    }
    [SerializeField]
    Canvas c;
    RectTransform rt;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // //Debug.Log(c.transform.InverseTransformPoint(transform.position));
            // Vector3 t;
            //// RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, rt.anchoredPosition, Camera.main,out t);
            // Debug.Log(RectTransformToScreenSpace(rt));
            // Debug.Log("Top" + Util.DisplayWorldCorners(rt, Util.CornerRectangle.top));
          Debug.Log(  WorldToScreenPoint(null, rt.anchoredPosition3D));
            Vector3[] corners = new Vector3[4];
     rt.GetWorldCorners(corners);
            Rect newRect = new Rect(corners[0], corners[2] - corners[0]);
            Debug.Log(newRect+ "  "+ Input.mousePosition + "   "+newRect.Contains(Input.mousePosition));

        }
           // Debug.Log(rt.rect);
         //  Debug.Log( RectTransformUtility.PixelAdjustPoint(rt.anchoredPosition,transform, c));
       // DisplayWorldCorners();
    }
       public static Vector2 WorldToScreenPoint(Camera cam, Vector3 worldPoint)
    {
        if ((Object)cam == (Object)null)
            return new Vector2(worldPoint.x, worldPoint.y);
        return (Vector2)cam.WorldToScreenPoint(worldPoint);
    }
    public static Rect RectTransformToScreenSpace(RectTransform r)
    {

        var worldCorners = new Vector3[4];
        r.GetWorldCorners(worldCorners);
        var result = new Rect(
                      worldCorners[0].x+r.sizeDelta.x,// 
                   Screen.width-(worldCorners[2].x),
                     -1,// left
                      -1);// top
        return result;
    }

  public static Rect RectTransformToScreenSpace1(RectTransform r)
    {

        var worldCorners = new Vector3[4];
        r.GetLocalCorners(worldCorners);
        var result = new Rect(
                      worldCorners[0].x,
                      worldCorners[0].y,
                      worldCorners[3].x,
                      worldCorners[3].y);
        return result;




    }
    void DisplayWorldCorners()
    {
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);

        Debug.Log("World Corners");
        for (var i = 0; i < 4; i++)
        {
            Debug.Log("World Corner " + i + " : " + v[i]);
        }
    }
}
