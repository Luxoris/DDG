using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

//http://wiki.unity3d.com/index.php?title=Saving_and_Loading_Data:_XmlSerializer
//https://learn.unity.com/tutorial/inheritance#5c8924f2edbc2a0d28f48439

[XmlRoot("dialogueEmeline")]
public class CMessage
{
    [XmlAttribute("id")] public string Id;
    [XmlElement("message")]
    public string Message;
    [XmlElement("next")]
    public string Next;

    public CMessage(): base()
    {
        Message = "Enter a message";
        Next = "Enter the next dialogue id";
    }

    public CMessage(string id, string txtMessage, string nextId)
    {
        this.Id = id;
        this.Next = nextId;
        this.Message = txtMessage;
    }

    override public string ToString()
    {
        return "Message : {"
            + "Message : " + this.Message
            + "Next : " + this.Next
            + " }";
    }


}
