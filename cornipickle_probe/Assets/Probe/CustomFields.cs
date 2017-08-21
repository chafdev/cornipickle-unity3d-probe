using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomFields : MonoBehaviour
{

    string id = "";

    public string Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }
}
