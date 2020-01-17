using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[XmlRoot("reponse")]
public class Reponse
{
    [XmlElement("valueChange")]
    public double ValueChange;
    [XmlElement("txtReponse")]
    public string TxtReponse;
    [XmlElement("next")]
    public string Next;

    public Reponse()
    {
        this.ValueChange = 0;
        this.TxtReponse = "Texte de réponse";
        this.Next = "Id du dialogue suivant";
    }

    public Reponse(double valueChange, string textReponse, string nextId)
    {
        ValueChange = valueChange;
        TxtReponse = textReponse;
        Next = nextId;
    }

    override public string ToString()
    {
        return "Message : {"
            + "ValueChange : " + this.ValueChange
            + "TxtReponse : " + this.Next
            + "Next : " + this.Next
            + " }";
    }
}
