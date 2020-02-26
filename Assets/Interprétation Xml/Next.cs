using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[XmlRoot("next")]
public class Next
{
    [XmlAttribute("ptMin")] public int nMin;
    [XmlText(typeof(string))]
    public string next;


    public Next()
    {
        next = "";
        nMin = 0;
    }

    public Next(int nMin, string next)
    {
        this.next = next;
        this.nMin = nMin;
    }

    override public string ToString()
    {
        string retour = "Next : {";
        retour += "Next = "+next;
        retour += " nMin = " + nMin;
        retour += " }";
        return retour;
    }
}
