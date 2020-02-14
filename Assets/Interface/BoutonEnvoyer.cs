using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoutonEnvoyer : MonoBehaviour
{
    public GameObject Game_Manager;
    public EventSystem es;
    public Scrollbar sb;
    //fonction ajout message
    public void AjoutMessage()
    {
        sb.value = 1;
        if (Game_Manager.GetComponent<Game.Game>().NumReponseSelectionne != -1)
        {    
            Game_Manager.GetComponent<Game.Game>().UI_CHOIX_CANEVAS.GetComponent<CreateButton>().destructionBoutonChoix();
            StartCoroutine(Game_Manager.GetComponent<Game.Game>().NextWithDelay(0f, Game_Manager.GetComponent<Game.Game>().NumReponseSelectionne));
            //Game_Manager.GetComponent<Game.Game>().Next(Game_Manager.GetComponent<Game.Game>().NumReponseSelectionne);
            Game_Manager.GetComponent<Game.Game>().NumReponseSelectionne = -1;
            es.SetSelectedGameObject(null);
        }
    }

    // Start is called before the first frame update


    public void Start()
    {
        
    }

}
