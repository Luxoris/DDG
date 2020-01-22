using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
//https://gram.gs/gramlog/xml-serialization-and-deserialization-in-unity/
//http://wiki.unity3d.com/index.php?title=Saving_and_Loading_Data:_XmlSerializer
//https://learn.unity.com/tutorial/inheritance#5c8924f2edbc2a0d28f48439

namespace Game
{
    [XmlRoot("script")]
    public class InterpretationXml
    {
        [XmlElement("dialogueEmeline")]
        public List<CMessage> Messages = new List<CMessage>();

        [XmlElement("dialogueJoueur")]
        public List<DialogueJoueur> ListeDialogueJoueurs = new List<DialogueJoueur>();

        public static InterpretationXml LoadXml(string path)
        {
            Debug.Log("Création du Serializer");
            XmlSerializer serializer = new XmlSerializer(typeof(InterpretationXml));
            Debug.Log("Ouverture du fichier xml");
            using (var stream = new FileStream(path, FileMode.Open))
            {
                Debug.Log("Désérialisation du fichier XML");
                return (InterpretationXml)serializer.Deserialize(stream);
            }
        }

        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(InterpretationXml));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
            
        }

        public void Add(CMessage m)
        {
            Messages.Add(m);
        }
    }

}

