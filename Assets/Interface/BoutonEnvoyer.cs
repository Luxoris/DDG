using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoutonEnvoyer : MonoBehaviour
{
    public GameObject Game_Manager;
    //fonction ajout message
    public void AjoutMessage()
    {
        Game_Manager.GetComponent<Game.Game>().Next(Game_Manager.GetComponent<Game.Game>().NumReponseSelectionne);
        Game_Manager.GetComponent<Game.Game>().NumReponseSelectionne = -1;
    }

    // Start is called before the first frame update

    private void Update()
    {
    }

}
