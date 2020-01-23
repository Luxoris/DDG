using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CreateButton : MonoBehaviour
{
    public GameObject UI_choix;
    public GameObject UI_choix_canvas;
    public GameObject Game_Manager;
    public GameObject UI_ScrollViewMessage;
    public List<GameObject> listeBouton = new List<GameObject>();

    void Start()
    {
        //creationBoutonChoix(3);
        //destructionBoutonChoix();
    }

    public void creationBoutonChoix(int nbChoix){
        int i = 0;
        float height = UI_choix.GetComponentInChildren<Text>().fontSize*2;
        
        Vector3 monVecteur = new Vector3(UI_choix_canvas.transform.position.x, 0 , 0f);
        for (i = 0; i < nbChoix; i++)
        {
            GameObject newButton = Instantiate(UI_choix, monVecteur, Quaternion.identity, UI_choix_canvas.transform);
            newButton.GetComponent<RectTransform>().offsetMax = new Vector2(0, -(i) * (height* 1f));
            newButton.GetComponent<RectTransform>().offsetMin = new Vector2(0, ((nbChoix - i -1f) * (height*1f)));
            
            //ajout du numéro du bouton
            newButton.GetComponent<UI_choix>().numBouton = i;
            //ajout de la référence de la réponse aux boutons
            newButton.GetComponent<UI_choix>().Game_Manager = Game_Manager;

            listeBouton.Add(newButton);
            //newButton.transform.Find("Text").GetComponent<Text>().text = width.ToString();

        }
        UI_choix_canvas.GetComponent<RectTransform>().offsetMax = new Vector2(UI_choix_canvas.GetComponent<RectTransform>().offsetMax.x, (height * (nbChoix+0.25f)));
        UI_ScrollViewMessage.GetComponent<RectTransform>().offsetMin = new Vector2(UI_ScrollViewMessage.GetComponent<RectTransform>().offsetMin.x, (height * (nbChoix + 0.25f)));
        
    }

    public void destructionBoutonChoix()
    {
        foreach (GameObject bouton in listeBouton)
        {
            Destroy(bouton);
        }
        listeBouton.Clear();
    }
}