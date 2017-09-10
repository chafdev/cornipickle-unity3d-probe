using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    public string data = "";
    RequestResponse _rrAdd = new RequestResponse();
    RequestResponse _rrImage = new RequestResponse();
    RequestResponse _rrCurent = new RequestResponse();
    [SerializeField]
    Probe _probe;
    [SerializeField]
    TextAsset txtProb;

    public void Start()
    {

        Debug.Log(txtProb.text);

        _rrAdd.set("http://localhost:10101/addMobile", txtProb.text, RequestName.add);
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
