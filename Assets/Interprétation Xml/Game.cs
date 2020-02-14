using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class Game : MonoBehaviour
    {
        public List<DialogueJoueur> ListeDialogueJoueurs = new List<DialogueJoueur>();
        public List<CMessage> ListeMessage = new List<CMessage>();
        public List<Jour> ListeJours = new List<Jour>();
        public int points =0;
        private string xmlPath;
        private string xmlPathSave;
        public GameObject UI_CHOIX_CANEVAS;
        public GameObject UI_CONTENT;
        public Slider UI_SliderSpeed;
        public CMessage TmpMessage;
        public DialogueJoueur TmpDialogueJoueur;
        public int NumReponseSelectionne =-1;
        private bool TmpIsMessage;
        public Save save = new Save();
        private int state = 0;
        private UnityWebRequest uwr;

        public float vitesseMessage = 0.025f;

        public Game()
        {
        }

        IEnumerator DownloadFile()
        {
            uwr = new UnityWebRequest("https://www.bruno-fache.studiofache.fr/dialogue.xml", UnityWebRequest.kHttpVerbGET);
            string path = xmlPath;
            uwr.downloadHandler = new DownloadHandlerFile(path);
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError || uwr.isHttpError)
                Debug.LogError(uwr.error);
            else
                Debug.Log("File successfully downloaded and saved to " + path);
        }

        public void ChargementXml()
        {
            InterpretationXml xml = new InterpretationXml();
            xml = InterpretationXml.LoadXml(xmlPath);
            //xml.Save(xmlPathSave);
            ListeMessage = xml.Messages;
            ListeDialogueJoueurs = xml.ListeDialogueJoueurs;
            ListeJours = xml.ListeJours;

            foreach(CMessage m in ListeMessage)
            {
                m.Next = m.Next.Replace(" ", "");
                m.Id = m.Id.Replace(" ", "");
            }

            foreach(DialogueJoueur d in ListeDialogueJoueurs)
            {
                d.Id = d.Id.Replace(" ", "");
                foreach(Reponse r in d.Reponses)
                {
                   r.Next = r.Next.Replace(" ", "");
                }
            }

            foreach (Jour j in ListeJours)
            {
                j.Id = j.Id.Replace(" ", "");
                foreach (Next n in j.Nexts)
                {
                    n.next = n.next.Replace(" ", "");
                }
            }

            foreach(Jour jour in ListeJours)
            {
                Debug.Log(jour.ToString());
            }
        }

        public void ChargementSauvegarde()
        {

            if (!System.IO.File.Exists(xmlPathSave))
            {
                save.SaveXml(xmlPathSave);

            }
            //chargement de la sauvegarde
            save = save.LoadXml(xmlPathSave);
            StartCoroutine(DelayChargementMessagesSauvegardees(1f));
            
        }

        public IEnumerator DelayChargementMessagesSauvegardees(float time)
        {
            //print(Time.time);
            yield return new WaitForSeconds(time);
            this.chargementMessagesSauvegardees();
        }
        public void chargementMessagesSauvegardees()
        {
            string TmpType = "";
            Debug.Log("Initialisation du premier message.");
            TmpType = GetById("1", ref TmpMessage, ref TmpDialogueJoueur);
            if (TmpType == "")
            {
                Debug.Log("L'Id ne correspond à aucun message.");
            }
            if (TmpType == "CMessage")
            {
                TmpIsMessage = true;
                if (TmpMessage.Date != "")
                {
                    UI_CONTENT.GetComponent<AjoutMessage>().AjoutDate(TmpMessage.Date);
                }
                UI_CONTENT.GetComponent<AjoutMessage>().AjoutMessageRecu(TmpMessage.Message, 20f);


                //this.Next(0);
            }
            if (TmpType == "DialogueJoueur")
            {
                TmpIsMessage = false;
                ajoutTextBoutonChoix();
            }

            int i = 0;
            while (i < (save.actions.Count - 1))
            {
                //gestion d'erreur si le message pointe vers lui-même :
                if (TmpMessage.Id == TmpMessage.Next)
                {
                    Debug.LogWarning("L'id du message " + TmpMessage.Id + "pointe vers lui-même.");
                }

                //Si c'est un message envoyé, ajoute le message dans l'interface
                if (!TmpIsMessage)
                {
                    if (TmpDialogueJoueur.Date != "")
                    {
                        UI_CONTENT.GetComponent<AjoutMessage>().AjoutDate(TmpDialogueJoueur.Date);
                    }
                    UI_CONTENT.GetComponent<AjoutMessage>().AjoutMessageEnvoye(TmpDialogueJoueur.Reponses[save.actions[i]].TxtReponse);

                    i++;
                }

                //appel le prochain message suivant si il est envoyé ou reçu
                string Next = "";
                if (TmpIsMessage)
                {
                    Next = TmpMessage.Next;
                }
                else
                {
                    Next = TmpDialogueJoueur.Reponses[save.actions[i - 1]].Next;

                }
                TmpType = GetById(Next, ref TmpMessage, ref TmpDialogueJoueur);
                if (TmpType == "CMessage")
                {
                    TmpIsMessage = true;
                    if (TmpMessage.Date != "")
                    {
                        UI_CONTENT.GetComponent<AjoutMessage>().AjoutDate(TmpMessage.Date);
                    }
                    UI_CONTENT.GetComponent<AjoutMessage>().AjoutMessageRecu(TmpMessage.Message);

                }
                if (TmpType == "DialogueJoueur")
                {
                    TmpIsMessage = false;
                }
                if (TmpType == "")
                {
                    Debug.LogWarning("La référence du next " + Next + " n'existe pas !");
                }
            }
            this.Next(0);
        }

        public void Start()
        {

            xmlPath = Application.persistentDataPath + "/dialogue.xml";
            xmlPathSave = Application.persistentDataPath + "/save.xml";
            NumReponseSelectionne = -1;
            UI_SliderSpeed.value = vitesseMessage * 200;

            //UI_CONTENT.GetComponent<AjoutMessage>().AjoutMessageRecu("Début du téléchargement");
            ////
            if (!System.IO.File.Exists(xmlPath))
            {
                StartCoroutine(DownloadFile());
            }
            else
            {
                state = 1;
            }
            
        }

        private void Update()
        {
            if (state == 0)
            {
                if (uwr!=null&&uwr.isDone&&System.IO.File.Exists(xmlPath))
                {
                    state = 1;
                    //UI_CONTENT.GetComponent<AjoutMessage>().AjoutMessageRecu("Telechargement reussi");
                }

            }
            if (state == 1)
            {
                ////
                
                Debug.Log("Chargement XML");
                ChargementXml();
                //Debug.Log("Le message actuel est un Message ? "+TmpIsMessage);
                ChargementSauvegarde();
                state = 2;
            }

        }

        public IEnumerator NextWithDelay(float time, int numBouton=0)
        {
            //print(Time.time);
            yield return new WaitForSeconds(time);
            this.Next(numBouton);
        }

        public IEnumerator NextAjoutMessage(float time, int numBouton, string Next)
        {
            //print(Time.time);
            yield return new WaitForSeconds(time);
            this.AjoutMessages(numBouton, Next);
        }

        public void AjoutMessages(int NumBouton, string Next)
        {
            float time = 1f;
            string TmpType = GetById(Next, ref TmpMessage, ref TmpDialogueJoueur);
            if (TmpType == "CMessage")
            {
                TmpIsMessage = true;
                if (TmpMessage.Date != "")
                {
                    UI_CONTENT.GetComponent<AjoutMessage>().AjoutDate(TmpMessage.Date);
                }
                UI_CONTENT.GetComponent<AjoutMessage>().AjoutMessageRecu(TmpMessage.Message);
                time = TmpMessage.Message.Length * vitesseMessage + (0.1f * 200 * vitesseMessage);
                StartCoroutine(NextWithDelay(time));
                //this.Next(0);
            }
            if (TmpType == "DialogueJoueur")
            {
                TmpIsMessage = false;
                ajoutTextBoutonChoix();
            }
            if (TmpType == "")
            {
                Debug.LogWarning("La référence du next " + Next + " n'existe pas !");
            }
        }
    
        public void Next(int NumBouton)
        {
            float time = vitesseMessage;
            if (NumBouton != -1)
            {
                //Debug.Log("Affichage du prochain message.");
                //gestion d'erreur si le message pointe vers lui-même :
                if (TmpMessage.Id == TmpMessage.Next)
                {
                    Debug.LogWarning("L'id du message " + TmpMessage.Id + "pointe vers lui-même.");
                }

                //Si c'est un message envoyé, ajoute le message dans l'interface
                if (!TmpIsMessage)
                {
                    if (TmpDialogueJoueur.Date != "")
                    {
                        UI_CONTENT.GetComponent<AjoutMessage>().AjoutDate(TmpDialogueJoueur.Date);
                    }
                    UI_CONTENT.GetComponent<AjoutMessage>().AjoutMessageEnvoye(TmpDialogueJoueur.Reponses[NumBouton].TxtReponse);
                    this.points += TmpDialogueJoueur.Reponses[NumBouton].ValueChange;
                    Debug.Log(this.points);

                    save.addSaveAction(NumBouton, xmlPathSave);

                    //gestion timer
                    time = TmpDialogueJoueur.Reponses[NumBouton].TxtReponse.Length * vitesseMessage + (0.1f * 200 * vitesseMessage);
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
                StartCoroutine(NextAjoutMessage(time, NumBouton, Next));
            }
        }

        public CMessage GetCMessageById(string id)
        {
            
            foreach (CMessage message in this.ListeMessage)
            {
                if (message.Id == id)
                {
                    return message;
                }
            }
            Debug.LogError("L'id n'existe pas dans la liste des messages d'émeline");
            return new CMessage("-1","" ,"L'id " + id + " n'existe pas dans la liste des messages d'émeline.", "-1");
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
            foreach (Jour j in this.ListeJours)
            {
                if(j.Id == id)
                {
                    return GetById(j.getBranchID(points), ref cMessage, ref dialogueJoueur);
                }
            }

            /*if (nbGetById < 2)
            {

                string[] tmpId2tab = id.Split('-');
                string tmpId2 = id;
                if (tmpId2tab.Length == 2)
                {
                    tmpId2 = tmpId2tab[0] + (int.Parse(tmpId2tab[1]) + 1).ToString();
                    tmpId2 = GetById(tmpId2, ref cMessage, ref dialogueJoueur);
                }
                if (tmpId2 != "")
                {
                    nbGetById = 0;
                    return tmpId2;
                }
                else
                {
                    string tmpId = id.Substring(0, 2);
                    tmpId = id[0] + (int.Parse(id[1].ToString()) + 1).ToString();
                    tmpId = GetById(tmpId, ref cMessage, ref dialogueJoueur);
                    if (tmpId != "")
                    {
                        nbGetById = 0;
                        return tmpId;
                    }
                    nbGetById++;
                }
            }*/
            return "";
        }

        public void ajoutTextBoutonChoix()
        {
            CreateButton butContainer = UI_CHOIX_CANEVAS.GetComponent<CreateButton>();
            butContainer.destructionBoutonChoix();
            butContainer.creationBoutonChoix(TmpDialogueJoueur.Reponses.Count, TmpDialogueJoueur.Reponses.ToArray());
            //int i = 0;
            //Debug.Log("NB reponses = " + TmpDialogueJoueur.Reponses.Count);
            /*if (TmpDialogueJoueur.Reponses.Count > 0)
            {
                i = 0;
                foreach (GameObject bouton in butContainer.GetComponent<CreateButton>().listeBouton)
                {
                    //Debug.Log("Num reponse = " + i);
                    bouton.GetComponent<UI_choix>().Text = TmpDialogueJoueur.Reponses[i].TxtReponse;
                    i++;
                }
            }*/

        }

        public void ResetSave()
        {
            System.IO.File.Delete(xmlPath);
            save.actions.Clear();
            save.SaveXml(xmlPathSave);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void setVitesseMessage()
        {
            vitesseMessage = UI_SliderSpeed.value * 0.005f;
        }
    }
}

