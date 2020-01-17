using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[XmlRoot("dialogueJoueur")]
public class DialogueJoueur 
{
    [XmlAttribute("id")] public string Id;

    [XmlElement("reponse")]
    public List<Reponse> Reponses= new List<Reponse>();

    public DialogueJoueur()
    {
    }
    public DialogueJoueur(string id,List<Reponse> reponses)
    {
        this.Id = id;
        this.Reponses = reponses;
    }

    public DialogueJoueur(string id,Reponse[] reponses)
    {
        this.Id = id;
        foreach (Reponse reponse in reponses)
        {
            this.Reponses.Add(reponse);
        }
    }

    override public string ToString()
    {
        string retour = "DialogueJoueur : {";
        foreach (Reponse reponse in Reponses)
        {
            retour += reponse;
        }
        retour += " }";
        return retour;
    }

}
