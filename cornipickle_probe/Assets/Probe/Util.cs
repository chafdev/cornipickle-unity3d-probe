using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour
{
    public string data = "";
    RequestResponse _rrAdd = new RequestResponse();
    RequestResponse _rrImage = new RequestResponse();
    RequestResponse _rrCurent = new RequestResponse();
    [SerializeField]
    Probe _probe;
    #region declaration
    string sb = "# We define a predicate using the construct\r\n" +
"# \"We say that <arguments> is/are <predicate name> when\".\r\n" +
"We say that $x and $y are left-aligned when (\r\n" +
"  $x's left equals $y's left\r\n" +
").\r\n" +
"We say that $x and $y are top-aligned when (\r\n" +
"  $x's top equals $y's top\r\n" +
").\r\n" +
"# We then express the fact that all menu items are either\r\n" +
"# left- or top-aligned.\r\n" +
"\"\"\"\r\n" +
"  @name Menus aligned\r\n" +
"  @description All list items should either be left- or top-aligned.\r\n" +
"  @severity Warning\r\n" +
"\"\"\"\r\n" +
"For each $z in $(Toggle Text) (\r\n" +
"  For each $t in $(Toggle Text) (\r\n" +
"    ($z and $t are left-aligned)\r\n" +
"    Or\r\n" +
"    ($z and $t are top-aligned)\r\n" +
"  )\r\n" +
").\r\n";
    #endregion
    public void Start()
    {
        _rrAdd.set("http://localhost:10101/addMobile", sb, RequestName.add);
        _rrImage.set("http://localhost:10101/imageMobile/", "", RequestName.image);

        // data = sb;
        _rrCurent = _rrAdd;
        StartCoroutine(download());

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Debug.Log("Pressed left click.");

        if (Input.GetMouseButtonDown(1))
            Debug.Log("Pressed right click.");

        if (Input.GetMouseButtonDown(2))
            Debug.Log("Pressed middle click.");

    }
    public static Vector2 getAspectRatio(Vector2 xy)
    {
        float f = xy.x / xy.y;
        int i = 0;
        /*   while (true)
           {
               i++;
               if (System.Math.Round(f * i, 2) == Mathf.RoundToInt(f * i))
                   break;
           }*/
        return new Vector2((float)System.Math.Round(f * i, 2), i);
    }
    public enum RequestName
    {
        add,
        image
    }
    public IEnumerator download()
    {


        Debug.Log("OS TYPE is Android" + getChemin());

        byte[] myData = System.Text.Encoding.UTF8.GetBytes(UrlUtility.UrlPathEncode(_rrCurent._dataToSend));

        // Debug.Log(androidFilePath);
        WWW www = new WWW(_rrCurent._url, myData);

        yield return www;
        while (www.isDone == false)
        {
            Debug.Log("Aucun response");
            throw new Exception("problem download");
        }
        if (www.isDone)
        {
            Debug.Log(www.bytesDownloaded);
            Debug.Log(www.text.ToString());
            // System.IO.File.WriteAllBytes(getChemin(), www.bytes);
            // startToload();
            //  string realPath = Application.persistentDataPath + name;
            //else

            JSONObject jsonObj = new JSONObject(www.text);
            // Getting JSON Array node

            if (_rrCurent.isRequestAdd())
            {
                JSONObject attrs = jsonObj.GetField("attributes");
                JSONObject arrTags = jsonObj.GetField("elements");

                String _inter = jsonObj.GetField("interpreter").ToString();
                _probe.LstAttributes.Clear();
                _probe.LstContainer.Clear();
                foreach (JSONObject j in attrs.list)
                {
                    _probe.LstAttributes.Add(j.str.ToString());
                    Debug.Log(j.str.ToString());
                }
                foreach (JSONObject j in arrTags.list)
                {
                    _probe.LstContainer.Add(j.str.ToString());
                    Debug.Log(j.str.ToString());
                }

                _probe.start();
            }
            else
            {
                if (www.text != "")
                {

                    String _inter = jsonObj.GetField("global-verdict").str;
                    JSONObject _hightlight = jsonObj.GetField("highlight-ids");//array



                }

            }
        }



    }

    private string getChemin()
    {
        //throw null;
        return null;
    }
    public class RequestResponse
    {
        public string _url = "";
        public string _dataToSend = "";
        bool _addPro = false;
        public RequestName _requestName = RequestName.add;

        public bool isRequestAdd()
        {

            return _requestName == RequestName.add;
        }

        public RequestResponse()
        {


        }
        public RequestResponse(string url, string _dataToSend)
        {

            this._url = url;
            this._dataToSend = _dataToSend;

        }
        public RequestResponse(string url, string _dataToSend, RequestName req)
        {

            this._url = url;
            this._dataToSend = _dataToSend;
            this._requestName = req;

        }

        public void set(string url, string _dataToSend, RequestName req)
        {

            this._url = url;
            this._dataToSend = _dataToSend;
            this._requestName = req;

        }
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

    internal static float getAbsoluteBottom( Component v)
    {
        return 1.0f;
    }

    internal static String getBackground(Component v)
    {
        return "rouge";
    }
}
