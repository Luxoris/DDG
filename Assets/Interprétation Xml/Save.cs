using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace Game
{
    [XmlRoot("Save")]
    public class Save
    {
        [XmlElement("dialogueEmeline")]
        public List<int> actions = new List<int>();

        public Save LoadXml(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Save));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                Debug.Log("Désérialisation du fichier XML");
                return (Save)serializer.Deserialize(stream);
            }
        }

        public void SaveXml(string path)
        {
            var serializer = new XmlSerializer(typeof(Save));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }

        }

        public void addSaveAction(int action, string path)
        {
            actions.Add(action);
            SaveXml(path);
        }
    }
    
}

