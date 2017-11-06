using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class DisplayProp : MonoBehaviour
{
    public TextAsset txt;
    public Text txtUi;
    public Text txtTitle;
    // Use this for initialization
    public void load()
    {
        txtUi.text = "";
        txtTitle.text = txt.name;
        TextAssetToList(txt);


    }
    private List<string> TextAssetToList(TextAsset ta)
    {
        var listToReturn = new List<string>();
        var arrayString = ta.text.Split('\n');
        foreach (var line in arrayString)
        {
            colorizer_comment(line);
            listToReturn.Add(line);
            // return listToReturn
        }
        return listToReturn;
    }

    public void colorizer_comment(string line)
    {

        line = setLine(line, "#80a", @"(#.+)");
        line = setLine(line, "#e09", @"(\sleft\s|\sright\s|\stop\s|\sbottom\s|\sheight\s|\swidth\s|\stext\s|\sid\s|\sclass\s|\sname\s|\sevent\s)");
        line = setLine(line, "green", @"(\sWe say that\s|\swhen\s)");
        line = setLine(line, "green", @"(For each|\sin\s)");
        line = setLine(line, "green", @"(There exists|such that)");
        line = setLine(line, "#44f", @"(\sAnd\s|\sOr\s)");
        line = setLine(line, "#44f", @"(\sequals\s|is greater than|is less than)");
        line = setLine(line, "#aaa", @"(\))");
        line = setLine(line, "#aaa", @"(\()");
       // Debug.Log(line);
        txtUi.text += line + "\n";


    }
    public string setLine(string line, string color, string pattern)
    {


        //   string pattern = @"(#.+)";
        Regex rgx = new Regex(pattern);
        return line = rgx.Replace(line, get(color)); //get("blue")
    }
    public string get(string color)
    {

        return @"<color=" + color + @">$1</color>";
    }
    // Update is called once per frame
    void Update()
    {

    }
}
