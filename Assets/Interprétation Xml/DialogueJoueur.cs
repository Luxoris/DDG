using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[XmlRoot("dialogueJoueur")]
public class DialogueJoueur 
{
    [XmlAttribute("id")] public string Id;

    [XmlElement("date")]
    public string Date;

    [XmlElement("reponse")]
    public List<Reponse> Reponses= new List<Reponse>();

    public DialogueJoueur()
    {
        this.Id = "0";
        this.Date = "";
        this.Reponses = new List<Reponse>();
    }
    public DialogueJoueur(string id,string date,List<Reponse> reponses)
    {
        this.Id = id;
        this.Date = date;
        this.Reponses = reponses;
    }

    public DialogueJoueur(string id, string date, Reponse[] reponses)
    {
        this.Id = id;
        foreach (Reponse reponse in reponses)
        {
            this.Reponses.Add(reponse);
        }
        this.Date = date;
    }

    override public string ToString()
    {
        string retour = "DialogueJoueur : {";
        foreach (Reponse reponse in Reponses)
        {
            retour += reponse;
        }
        retour += " }";
        retour += " Date = " + this.Date;
        return retour;
    }

}
