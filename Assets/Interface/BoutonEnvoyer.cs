using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoutonEnvoyer : MonoBehaviour
{
    public GameObject UI_Reponse;
    public GameObject UI_Content;
    //fonction ajout message
    public void AjoutMessage()
    {
        UI_Content.GetComponent<AjoutMessage>().AjoutMessageEnvoye(UI_Reponse.GetComponent<Text>().text);
        UI_Content.GetComponent<AjoutMessage>().AjoutMessageRecu("Message réponse de test)");
    }

    // Start is called before the first frame update

    private void Update()
    {
    }

}
