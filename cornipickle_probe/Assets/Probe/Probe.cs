﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Probe : MonoBehaviour
{

    /*
      *Ce analyse elements de chaque view recursivment et retourne tous les widgets
      *  les elements retournes sous forme json avec leur proprites
      *  @parm v :L'objet parent d'une activité
      *

   */
    /**
    * for generating id for each element in json file
    */
    int cornipickleid = 0;

    /// <summary>
    /// la liste des noms des root
    /// </summary>

    List<String> _lstNameRootCanvas = new List<string>();

    List<Canvas> _lstCanvasFiletred = new List<Canvas>();
    /**
   * A List of attributes to include.
   */

    List<String> lstAttributes = new List<String>();
    /**
     * A list of layouts ,container ,widgets to include in json file
     */

    List<String> lstContainer = new List<String>();
    /**
   * help to keep all element reference
   */
    Dictionary<int, GameObject> idMap = new Dictionary<int, GameObject>();

    public List<string> LstAttributes
    {
        get
        {
            return lstAttributes;
        }

        set
        {
            lstAttributes = value;
        }
    }

    public List<string> LstContainer
    {
        get
        {
            return lstContainer;
        }

        set
        {
            lstContainer = value;
        }
    }

    public bool containsCanvas(Canvas c1)
    {

        foreach (Canvas c in _lstCanvasFiletred)
        {
            if (c.transform.root.name == c1.transform.root.name)
            {
                return true;
            }
        }
        return false;
    }


    public void AddCanvas(Canvas c)
    {
        //&& c.gameObject.activeSelf && c.enabled
        if (!containsCanvas(c))
        {

            _lstCanvasFiletred.Add(c);
        }

    }
    class View : IEnumerable<View>, IComparable<View>, IEquatable<View>
    {
        public List<View> _children =
                                            new List<View>();

        public int ID
        {
            get { return gCurrent.GetInstanceID(); }
        }

        public readonly GameObject gCurrent;

        public View Parent { get; private set; }
        public View()
        {
        }

        public View(GameObject g)
        {
            this.gCurrent = g;
            // ID = g.GetInstanceID();
        }

        public View GetChild(GameObject g)
        {
            return this._children[ID];
        }

        public void Add(View item)
        {
            if (_children.Contains(item))
                return;

            if (item.Parent != null)
            {
                item.Parent._children.Remove(item);
            }

            item.Parent = this;
            this._children.Add(item);
        }

        public IEnumerator<View> GetEnumerator()
        {
            return this._children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int CompareTo(View other)
        {
            return this.gCurrent.GetInstanceID().CompareTo(other.gCurrent.GetInstanceID());
        }

        bool IEquatable<View>.Equals(View other)
        {
            return this.gCurrent.GetInstanceID().Equals(other.gCurrent.GetInstanceID());
        }

        public int Count
        {
            get { return this._children.Count; }
        }

        /*
         * 
         * 
         * var tree = new TreeNode("Root")
               {
                   new TreeNode("Category 1")
                       {
                           new TreeNode("Item 1"),
                           new TreeNode("Item 2"),
                           new TreeNode("Item 3"),
                       },
                   new TreeNode("Category 2")
                       {
                           new TreeNode("Item 1"),
                           new TreeNode("Item 2"),
                           new TreeNode("Item 3"),
                           new TreeNode("Item 4"),
                       }
               };
               */
    }

    class Viewl : Transform
    {

        public Transform _child;

        Transform _parent;



        List<Transform> lstTransform = new List<Transform>();

        Boolean canvasRoot = false;


        public void AddTransform(Transform t)
        {


            T.Add(t);
        }
        public List<Transform> T
        {
            get
            {
                return lstTransform;
            }

            set
            {
                lstTransform.AddRange(value);
            }
        }

        public bool CanvasRoot
        {
            get
            {
                return canvasRoot;
            }

            set
            {
                canvasRoot = value;
            }
        }
    }

  public  void start()
    {

        Canvas[] canvas = FindObjectsOfType(typeof(Canvas)) as Canvas[];
        //   View v = new View();

        // GameObject g = new GameObject();

        View v = new View(null);
        foreach (Canvas c1 in canvas)
        {
            AddCanvas(c1);
            // print(c1.transform;


        }
        foreach (Canvas c1 in _lstCanvasFiletred)
        {


            v.Add(new View(c1.gameObject));

        }

        // canvas[0].transform.root
        if (canvas.Length > 0)

            print(canvas[0].gameObject.name);



        JSONObject resultJson = new JSONObject(JSONObject.Type.OBJECT);
        // number
        // resultJson.AddField("element", "window");


        resultJson.AddField("element", "window");
        resultJson.AddField("aspect-ratio", Config.getAspectRatio(Vector2.zero).x);
        resultJson.AddField("orientation", "");
        resultJson.AddField("width", "");
        resultJson.AddField("height", "");
        resultJson.AddField("device-width", "");
        resultJson.AddField("device-height", "");
        resultJson.AddField("url", "");
        resultJson.AddField("device-langue", "");
        JSONObject arr = new JSONObject(JSONObject.Type.ARRAY);

        resultJson.AddField("children", arr);

        //analyseViews(canvas[0].transform,0, arr);
        analyseView(v, 0, arr, MotionEvent.non);
        Debug.Log(resultJson.ToString());
    }

    public bool isAttributeExists(String property_name)
    {


        if (lstAttributes.Contains(property_name))

            return true;

        return false;
    }
    void analyseView(View tc, int level, JSONObject jArray, MotionEvent evt)
    {


        foreach (View t in tc)
        {

            analyseViews(t.gCurrent.transform, 1, jArray, evt);
            /* foreach (View v in t._children)
             {
                 JSONObject jNodeChild = new JSONObject(JSONObject.Type.OBJECT);
                 jNodeChild.AddField("children", jArray);
                 analyseView(v,1, jArray);
                     }*/
        }



    }
   public enum MotionEvent
    {

        leftclick, rightclick, middle, non
    }



    public bool canIncludeThisView(JSONObject jNodeChild, Component t)
    {


        int id = t.GetInstanceID();
        String _element = t.GetType().Name;
        foreach (String s in lstContainer)
        {

            if (s.ToLower().Equals(_element.ToLower()))
            {
                try
                {
                    jNodeChild.AddField("element", _element);
                }
                catch (JSONException e)
                {

                }
                return true;
            }
            if ((t.gameObject.tag != null) && s.StartsWith(".") && s.ToLower().Equals("." + t.tag.ToLower()))
            {
                try
                {
                    jNodeChild.AddField("element", _element);
                    jNodeChild.AddField("tag", s.Substring(1));
                }
                catch (JSONException e)
                {

                }
                return true;
            }

            if (s.StartsWith("#") && t.GetComponent<CustomFields>() != null && (t.GetComponent<CustomFields>().Id.Substring(1)) == s)
            {
                try
                {
                    jNodeChild.AddField("element", _element);
                    jNodeChild.AddField("id", s.Substring(1));
                }
                catch (JSONException e)
                {

                }
                return true;
            }

        }


        return false;

    }


    public void analyseViews(Transform tc, int level, JSONObject jArray, MotionEvent evt)
    {

        JSONObject jNode = new JSONObject(JSONObject.Type.OBJECT);

  

        jArray.Add(jNode);

        JSONObject jArrayChild = new JSONObject(JSONObject.Type.ARRAY);

        //  Debug.Log(tc.name + " Level " + level);
        foreach (var component in tc.GetComponents<Component>())
        {
            if (canIncludeThisView(jNode, component))
            {
                jNode.AddField("name", tc.GetComponents<Component>()[0].name.ToString());
                addAttributeIfDefined(jNode, component, evt);
                break;
            }
            Debug.Log("1 Transform: " + tc.name + " Component: " + component.name + " level: " + level);
        }
        foreach (Transform t in tc)
        {

            JSONObject jNodeChild = new JSONObject(JSONObject.Type.OBJECT);

            if (t.childCount > 0)
            {

                jNodeChild.AddField("children", jArrayChild);

                analyseViews(t, level + 1, jArrayChild, evt);

            }
            else
            {
               
                foreach (var component in t.GetComponents<Component>())
                {
                    if (canIncludeThisView(jNodeChild, component))
                    {
                        //  level = level + 1;
                        jNodeChild.AddField("communt", t.GetComponents<Component>()[0].name.ToString());
                   
                        addAttributeIfDefined(jNodeChild, component, evt);
                        jArrayChild.Add(jNodeChild);
                    }
                    break;
                    Debug.Log("trasform-Parent: " + t.name + " Component: " + component.name + " level: " + level);
                }
            }


        }
        jNode.AddField("children", jArrayChild);


    }


    void addAttributeIfDefined(JSONObject jNodeChild, Component v, MotionEvent evt)
    {


        // jNodeChild.put("id", v.getId());
        cornipickleid = cornipickleid + 1;
        jNodeChild.AddField("cornipickleid", cornipickleid);
        idMap.Add(cornipickleid, v.gameObject);
        if (isAttributeExists("id"))
            if (v.GetComponent<CustomFields>() != null)
            {
                if (v.GetComponent<CustomFields>().Id != "")
                {

                    jNodeChild.AddField("id", v.GetComponent<CustomFields>().Id);
                }
            }
        RectTransform rt = v.gameObject.GetComponent<RectTransform>();
        if (rt)
        {

            if (isAttributeExists("width"))
                jNodeChild.AddField("width", rt.sizeDelta.x);

            if (isAttributeExists("height"))
                jNodeChild.AddField("height", rt.sizeDelta.y);

            //   int i1 = r.nextInt(500 - 20) + 20;
            if (isAttributeExists("left"))
                jNodeChild.AddField("left", Config.getAbsoluteLeft(v));
            if (isAttributeExists("right"))
                jNodeChild.AddField("right", Config.getAbsoluteRight(v));
            if (isAttributeExists("top"))
                jNodeChild.AddField("top", Config.getAbsoluteTop(v));
            if (isAttributeExists("bottom"))
                jNodeChild.AddField("bottom", Config.getAbsoluteBottom(v));
            if (isAttributeExists("size") && v.transform.childCount > 0)
                jNodeChild.AddField("size", v.transform.childCount);
            if (isAttributeExists("background"))
            {

                jNodeChild.AddField("background", Config.getBackground(v));

            }
            if (isAttributeExists("event"))
            {

                jNodeChild.AddField("event", evt.ToString());
            }
        }
        if (isAttributeExists("parent"))
        {

            // v.transform.parent.getClass().getSimpleName()
            jNodeChild.AddField("parent", "parent");

        }

    }

}


/*
 *   //    analyseViews(canvas[0].transform,0,arr);
        // Note: your data can only be numbers and strings.
        // This is not a solution for object serialization
        // or anything like that.
        /*  JSONObject j = new JSONObject(JSONObject.Type.OBJECT);
          // number
          j.AddField("field1", 0.5f);
          // string
          j.AddField("field2", "sampletext");
          // array
          JSONObject arr = new JSONObject(JSONObject.Type.ARRAY);
          j.AddField("field3", arr);

          arr.Add(1);
          arr.Add(2);
          arr.Add(3);
          // arr.AddField("test",j);
          JSONObject j1 = new JSONObject(JSONObject.Type.OBJECT);
          // number
          j1.AddField("f1", 0.5f);
          // string
          j1.AddField("f2", "sampletext");
          arr.Add(j1);
          Debug.Log(j.ToString());

          //string encodedString = j.print();*/


