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
    public static Config instance;
    public void Awake()
    {

        Debug.Log(txtProb.text);
        instance = this;
        _rrAdd.set("http://localhost:10101/addMobile", txtProb.text, RequestName.add);
        _rrImage.set("http://localhost:10101/imageMobile/", "", RequestName.image);

        // data = sb;
        _rrCurent = _rrAdd;
        StartCoroutine(download());

    }
    public void setRequest(String dt)
    {

        _rrImage._dataToSend = dt;
        _rrCurent = _rrImage;
        StartCoroutine(download());
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Debug.Log("Pressed left click.");
        if (Input.GetMouseButtonDown(1))
        {
            // _rrCurent = _rrImage;
            //StartCoroutine(download());
            _probe.start();
        }
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


        Debug.Log("downloading .. " + getChemin());
        byte[] myData = null;
        if (_rrCurent._requestName == RequestName.image)
        {
            Debug.Log("je vais");
            string datas = "contents=" + UrlUtility.UrlEncode(_rrCurent._dataToSend);
            datas += "&interpreter=" + UrlUtility.UrlEncode(_probe.interpreter);
            datas += "&id=" + 1;
            datas += "&hash=" + "hash";
            myData = System.Text.Encoding.UTF8.GetBytes(datas);
        }
        else

            myData = System.Text.Encoding.UTF8.GetBytes(UrlUtility.UrlEncode(_rrCurent._dataToSend));

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
                _probe.interpreter = _inter;
                _probe.LstAttributes.Clear();
                _probe.LstContainer.Clear();
                foreach (JSONObject j in attrs.list)
                {
                    _probe.LstAttributes.Add(j.str.ToString());
                    // Debug.Log(j.str.ToString());
                }
                foreach (JSONObject j in arrTags.list)
                {
                    _probe.LstContainer.Add(j.str.ToString());
                    // Debug.Log(j.str.ToString());
                }

                //_probe.start();
            }
            else
            {
                if (www.text != "")
                {

                    String _inter = jsonObj.GetField("global-verdict").str;
                    JSONObject _hightlight = jsonObj.GetField("highlight-ids");//array
                    Debug.Log("test" + _inter + " " + _hightlight.list.Count);


                    // Highlight elements, if any
                    for (int j = 0; j < _hightlight.list.Count; j++)
                    {

                        JSONObject set_of_tuples1 = (JSONObject)_hightlight[j];
                        List<JSONObject> ids = set_of_tuples1.GetField("ids").list;

                        String jlink = set_of_tuples1.GetField("link").ToString();

                        Debug.Log("ids " + ids.Count);

                        for (int z = 0; z < ids.Count; z++)
                        {
                            List<JSONObject> js2 = ids[z].list;
                            String st = js2[0].ToString();
                            int key1 = Convert.ToInt32(js2[0].ToString());
                            if (_probe.idMap.ContainsKey(key1))
                            {

                                print("Key1" + key1 + " " + _probe.idMap[key1]);
                            }
                            int key2 = Convert.ToInt32(js2[1].ToString());
                            if (_probe.idMap.ContainsKey(key2))
                            {

                                print("Key2" + key2 + " " + _probe.idMap[key2]);
                            }


                        }
                    }




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

    internal static float getAbsoluteBottom(Component v)
    {
        return 1.0f;
    }

    internal static String getBackground(Component v)
    {
        return "rouge";
    }
}
