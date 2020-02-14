using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("jour")]
public class Jour
{
    [XmlAttribute("id")] public string Id;

    [XmlElement("next")]
    public List<Next> Nexts = new List<Next>();

    public Jour()
    {
        Nexts = new List<Next>();
        Id = "";
    }
    public Jour(string id, List<Next> nexts)
    {
        Nexts = nexts;
        Id = id;
    }
    override public string ToString()
    {
        string retour = "Jour : {";
        foreach (Next next in Nexts)
        {
            retour += next;
        }
        retour += " }";
        retour += " Id = " + this.Id;
        return retour;
    }

    public string getBranchID(int score)
    {
        Next mem = new Next(0,"");
        foreach(Next next in Nexts)
        {
            if (score >= next.nMin)
            {
                mem = next;
            }
        }
        if (mem.next == "")
        {
            Debug.LogWarning("L'id du next du jour " + Id + " n'est pas défini.");
        }
        return mem.next;
    }
}

