using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Game
{
    public class Game: MonoBehaviour
    {
        public List<DialogueJoueur> ListeDialogueJoueurs = new List<DialogueJoueur>();
        public List<CMessage> ListeMessage = new List<CMessage>();
        public string[] cheminParcouru;
        public int points;
        public string xmlPath;
        public string xmlPathSave;
        public GameObject UI_CHOIX_CANEVAS;
        public GameObject UI_CONTENT;
        public CMessage TmpMessage;
        public DialogueJoueur TmpDialogueJoueur;
        public int NumReponseSelectionne;
        private bool TmpIsMessage;
        
        public Game(){ 
        }

        public void ChargementXml()
        {
            InterpretationXml xml = new InterpretationXml();
            xml = InterpretationXml.LoadXml(xmlPath);
            xml.Save(xmlPathSave);
            ListeMessage = xml.Messages;
            ListeDialogueJoueurs = xml.ListeDialogueJoueurs;
        }

        public void ChargementSauvegarde()
        {

        }

        public void Start()
        {
            string TmpType = "";
            xmlPath = Path.Combine(Application.dataPath, "Ressources/XML/dialogue.xml");
            xmlPathSave = Path.Combine(Application.dataPath, "Ressources/XML/save.xml");
            Debug.Log("Chargement XML");
            ChargementXml();
            Debug.Log("Initialisation du premier message.");
            TmpType = GetById("1", ref TmpMessage, ref TmpDialogueJoueur);
            if (TmpType=="")
            {
                Debug.Log("L'Id ne correspond à aucun message.");
            }
            if (TmpType == "CMessage")
            {
                TmpIsMessage = true;
                UI_CONTENT.GetComponent<AjoutMessage>().AjoutMessageRecu(TmpMessage.Message);
                this.Next(0);
            }
            if (TmpType == "DialogueJoueur")
            {
                TmpIsMessage = false;
                ajoutTextBoutonChoix();
            }
            Debug.Log("Le message actuel est un Message ? "+TmpIsMessage);

        }

        public void Next(int NumBouton)
        {
            Debug.Log("Affichage du prochain message.");
            //gestion d'erreur si le message pointe vers lui-même :
            if(TmpMessage.Id == TmpMessage.Next)
            {
                Debug.LogWarning("L'id du message " + TmpMessage.Id + "pointe vers lui-même.");
            }

            //Si c'est un message envoyé, ajoute le message dans l'interface
            if (!TmpIsMessage)
            {
                UI_CONTENT.GetComponent<AjoutMessage>().AjoutMessageEnvoye(TmpDialogueJoueur.Reponses[NumBouton].TxtReponse);
            }

            //appel le prochain message suivant si il est envoyé ou reçu
            string Next = "";
            if (TmpIsMessage)
            {
                Next = TmpMessage.Next;
            }
            else
            {
                Next = TmpDialogueJoueur.Reponses[NumBouton].Next;

            }
            string TmpType = GetById(Next, ref TmpMessage, ref TmpDialogueJoueur);
            if (TmpType == "CMessage")
            {
                TmpIsMessage = true;
                UI_CONTENT.GetComponent<AjoutMessage>().AjoutMessageRecu(TmpMessage.Message);
                this.Next(0);
            }
            if (TmpType == "DialogueJoueur")
            {
                TmpIsMessage = false;
                ajoutTextBoutonChoix();
            }
            if(TmpType == "")
            {
                Debug.LogWarning("La référence du next "+Next+" n'existe pas !");
            }
        }

        public CMessage GetCMessageById(string id)
        {
            foreach(CMessage message in this.ListeMessage)
            {
                if (message.Id == id)
                {
                    return message;
                }
            }
            Debug.LogError("L'id n'existe pas dans la liste des messages d'émeline");
            return new CMessage("-1", "L'id " + id + " n'existe pas dans la liste des messages d'émeline.", "-1");
        }

        public DialogueJoueur GetDialogueById(string id)
        {
            foreach (DialogueJoueur dial in this.ListeDialogueJoueurs)
            {
                if (dial.Id == id)
                {
                    return dial;
                }
            }
            Debug.LogError("L'id n'existe pas dans la liste des dialogues joueurs.");
            return new DialogueJoueur();
        }


        public string GetById(string id, ref CMessage cMessage, ref DialogueJoueur dialogueJoueur)
        {
            foreach (CMessage message in this.ListeMessage)
            {
                if (message.Id == id)
                {
                    cMessage = message;
                    return "CMessage";
                }
            }
            foreach (DialogueJoueur dial in this.ListeDialogueJoueurs)
            {
                if (dial.Id == id)
                {
                    dialogueJoueur = dial; 
                    return "DialogueJoueur";
                }
            }
            return "";
        }

        public void ajoutTextBoutonChoix()
        {
            CreateButton butContainer = UI_CHOIX_CANEVAS.GetComponent<CreateButton>();
            butContainer.destructionBoutonChoix();
            butContainer.creationBoutonChoix(TmpDialogueJoueur.Reponses.Count);
            int i = 0;
            foreach(Transform child in butContainer.transform)
            {
                child.gameObject.GetComponent<UI_choix>().Text = TmpDialogueJoueur.Reponses[i].TxtReponse;
                i++;
            }
            
        }
    }


}

