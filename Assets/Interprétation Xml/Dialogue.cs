using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

public class Dialogue
{
    [XmlAttribute("id")] public string Id;

    public Dialogue(){
        Id = "default";
    }
    public Dialogue(string id)
    {
        Id = id;
    }

    public void Add(string id)
    {
        Id = id;
    }
    public override bool Equals(object obj)
    {
        return obj is Dialogue dialogue &&
               base.Equals(obj) &&
               Id == dialogue.Id;
    }

    public override int GetHashCode()
    {
        var hashCode = 1545243542;
        hashCode = hashCode * -1521134295 + base.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
        return hashCode;
    }
}