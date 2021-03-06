﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Config : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnKeyForBug;
    public static event ClickAction OnKeyForReset;
    RequestResponse _rrAdd = new RequestResponse();
    RequestResponse _rrImage = new RequestResponse();
    RequestResponse _rrCurent = new RequestResponse();
    [SerializeField]
    GameObject __hprobe;
    [SerializeField]
    GameObject _response;
    [SerializeField]
    GameObject _responseI;
    [SerializeField]
    Probe _probe;
    [SerializeField]
    TextAsset txtProb;
    [SerializeField]
    String nameProp;
    [SerializeField]
    PosLayoutResult posLayoutResponse = PosLayoutResult.left_top;
    List<GameObject> lstCadre = new List<GameObject>();
    public GameObject canvasProbe;
    GameObject canvasProbeCurrent;
    public GameObject gPopup;
    GameObject gPopupInstance;
    public static Config instance;
    enum PosLayoutResult
    {
        right_top,
        right_bottom,
        left_top,
        left_bottom
    }
    Vector2 setPosLayoutResult(PosLayoutResult pos)
    {
        String p = pos.ToString();

        switch (p)
        {
            case "right_top":
                {
                    return new Vector2(Screen.width - 25, -25);
                }

            case "right_bottom":
                {
                    return new Vector2(Screen.width - 25, 25 - Screen.height);
                }

            case "left_top":
                {
                    return new Vector2(25, -25);
                }

            case "left_bottom":
                {
                    return new Vector2(25, 25 - Screen.height);
                }
            default:
                {
                    return new Vector2(25, -25);
                }
        }
    }
    public void Awake()
    {
        canvasProbeCurrent = Instantiate(canvasProbe);
        instance = this;
        _rrImage.set("http://localhost:10101/imageMobile/", "", RequestName.image);
        loadProperties();

    }
    void Start()
    {


        if (_responseI == null)
        {
            _responseI = Instantiate(_response);
            //_responseI.transform.SetParent(_probe.vCurent.gCurrent.transform);
            _responseI.transform.SetParent(canvasProbeCurrent.transform);
            _responseI.transform.localScale = Vector3.one;
            _responseI.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
            _responseI.GetComponent<RectTransform>().anchoredPosition = setPosLayoutResult(posLayoutResponse);
        }
    }

    public void loadProperties(String namep)
    {
        txtProb = Resources.Load("props/" + namep, typeof(TextAsset)) as TextAsset;
        _rrAdd.set("http://localhost:10101/addMobile", txtProb.text, RequestName.add);
        _rrCurent = _rrAdd;
        StartCoroutine(download());
    }
    public void loadProperties()
    {
        if (txtProb == null)
            txtProb = Resources.Load("props/" + nameProp, typeof(TextAsset)) as TextAsset;
        _rrAdd.set("http://localhost:10101/addMobile", txtProb.text, RequestName.add);
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

        if (Input.GetMouseButtonDown(0) || Input.GetKeyUp(KeyCode.N) || Input.GetKeyUp(KeyCode.B))
        {
            _probe.start();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (OnKeyForBug != null)
                OnKeyForBug();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (OnKeyForReset != null)
                OnKeyForReset();
        }
        if (Input.GetKeyDown(KeyCode.X))
            SceneManager.LoadScene(0);


    }
    public void showPopup(Boolean show)
    {

        if (show)
        {
            if (gPopupInstance == null)
            {
                gPopupInstance = Instantiate(gPopup);

                gPopupInstance.transform.SetParent(canvasProbeCurrent.transform);
                gPopupInstance.transform.localScale = Vector3.one;
                gPopupInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(-300, -200);
                gPopupInstance.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 400);



            }
            gPopupInstance.GetComponent<DisplayProp>().txt = txtProb;

            gPopupInstance.GetComponent<DisplayProp>().load();
        }
        gPopupInstance.SetActive(show);
    }



    public static Vector2 getAspectRatio(Vector2 xy)
    {
        float f = xy.x / xy.y;
        int i = 0;
        return new Vector2((float)System.Math.Round(f * i, 2), i);
    }
    public enum RequestName
    {
        add,
        image
    }
    public IEnumerator download()
    {
       // Debug.Log("downloading .. " + getChemin());
        byte[] myData = null;
        if (_rrCurent._requestName == RequestName.image)
        {
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
            //Debug.Log(www.bytesDownloaded);
            //   Debug.Log(www.text.ToString());
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
                  //  Debug.Log(j.str.ToString());
                }
                foreach (JSONObject j in arrTags.list)
                {
                    _probe.LstContainer.Add(j.str.ToString());
                  //  Debug.Log(j.str.ToString());
                }

                //_probe.start();
            }
            else
            {
                if (www.text != "")
                {

                    String _result = jsonObj.GetField("global-verdict").str;
                    JSONObject _hightlight = jsonObj.GetField("highlight-ids");//array
                                                                               //   Debug.Log("test" + _result + " " + _hightlight + " ");
                                                                               /*   foreach (int i in _probe.idMap.Keys)
                                                                                      Debug.Log(i);*/

                    if (_responseI == null)
                    {
                        _responseI = Instantiate(_response);
                        //_responseI.transform.SetParent(_probe.vCurent.gCurrent.transform);
                        _responseI.transform.SetParent(canvasProbeCurrent.transform);
                        _responseI.transform.localScale = Vector3.one;
                        _responseI.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
                    }
                    _responseI.GetComponent<RectTransform>().anchoredPosition = setPosLayoutResult(posLayoutResponse);
                    if (_result.ToLower().Equals("true"))
                    {
                        _responseI.GetComponent<Image>().color = Color.green;
                    }
                    else
                    {
                        _responseI.GetComponent<Image>().color = Color.red;
                    }
                    foreach (GameObject t in lstCadre)
                    {

                        DestroyImmediate(t);

                    }
                    lstCadre.Clear();

                    // Highlight elements, if any
                    for (int j = 0; j < _hightlight.list.Count; j++)
                    {

                        JSONObject set_of_tuples1 = (JSONObject)_hightlight[j];
                        List<JSONObject> ids = set_of_tuples1.GetField("ids").list;

                        String jlink = set_of_tuples1.GetField("link").ToString();

                        // Debug.Log("ids " + ids.Count + "  " + _probe.idMap.Count);


                        for (int z = 0; z < ids.Count; z++)
                        {
                            List<JSONObject> js2 = ids[z].list;
                            if (js2.Count > 0)
                            {
                                String st = js2[0].ToString();

                                int key1 = Convert.ToInt32(js2[0].ToString());
                                if (_probe.idMap.ContainsKey(key1))
                                {
                                    // print("yadkhol");
                                    if (!_probe.idMap[key1].transform.FindChild(__hprobe.name))
                                    {
                                        GameObject g = Instantiate(__hprobe);
                                        g.transform.name = __hprobe.name;
                                        g.transform.SetParent(_probe.idMap[key1].transform);
                                        g.GetComponent<RectTransform>().sizeDelta = Util.getWH(_probe.idMap[key1].GetComponent<RectTransform>()); ;
                                        g.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                                        // print("Key1" + key1 + " " + _probe.idMap[key1]);
                                        lstCadre.Add(g);
                                    }
                                }
                            }
                            if (js2.Count > 1)
                            {
                                int key2 = Convert.ToInt32(js2[1].ToString());
                                if (_probe.idMap.ContainsKey(key2))
                                {
                                    if (!_probe.idMap[key2].transform.FindChild(__hprobe.name))
                                    {
                                        GameObject g = Instantiate(__hprobe);
                                        g.transform.name = __hprobe.name;
                                        g.transform.SetParent(_probe.idMap[key2].transform);
                                        g.GetComponent<RectTransform>().sizeDelta =Util.getWH( _probe.idMap[key2].GetComponent<RectTransform>());
                                        g.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                                        lstCadre.Add(g);
                                    }

                                    //  print("Key2" + key2 + " " + _probe.idMap[key2]);
                                }
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


}
