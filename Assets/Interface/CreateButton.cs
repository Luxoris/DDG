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
        float height = 30;
        
        Vector3 monVecteur = new Vector3(UI_choix_canvas.transform.position.x, 0 , 0f);
        float posY = height *0.5f +1;
        for (i = 0; i < nbChoix; i++)
        {
            
            GameObject newButton = Instantiate(UI_choix, monVecteur, Quaternion.identity, UI_choix_canvas.transform);
            RectTransform t = newButton.GetComponent<RectTransform>();
            float posX = ((+t.offsetMin.x - t.offsetMax.x) * 0.5f);
            //newButton.GetComponent<RectTransform>().offsetMax = new Vector2(0, -(i) * (height* 0.75f) -5);
            //newButton.GetComponent<RectTransform>().offsetMin = new Vector2(0, ((nbChoix - i -1f) * (height*0.75f)) +5);

            newButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, posY);
            posY += height +2;

            //ajout du numéro du bouton
            newButton.GetComponent<UI_choix>().numBouton = i;
            //ajout de la référence de la réponse aux boutons
            newButton.GetComponent<UI_choix>().Game_Manager = Game_Manager;

            listeBouton.Add(newButton);
            //newButton.transform.Find("Text").GetComponent<Text>().text = width.ToString();

        }
        UI_choix_canvas.GetComponent<RectTransform>().offsetMax = new Vector2(UI_choix_canvas.GetComponent<RectTransform>().offsetMax.x, ((height) * (nbChoix+1)));
        UI_ScrollViewMessage.GetComponent<RectTransform>().offsetMin = new Vector2(UI_ScrollViewMessage.GetComponent<RectTransform>().offsetMin.x, ((height) * (nbChoix+1)));
        
    }

    public void destructionBoutonChoix()
    {
        foreach (GameObject bouton in listeBouton)
        {
            Destroy(bouton);
        }
        listeBouton.Clear();
        //UI_choix_canvas.GetComponent<RectTransform>().offsetMax = new Vector2(UI_choix_canvas.GetComponent<RectTransform>().offsetMax.x, (0));
        //UI_ScrollViewMessage.GetComponent<RectTransform>().offsetMin = new Vector2(UI_ScrollViewMessage.GetComponent<RectTransform>().offsetMin.x, (30));

    }
}