using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateButton : MonoBehaviour
{
    public GameObject UI_choix;
    public GameObject UI_choix_canvas;
    public GameObject Game_Manager;

    void Start()
    {
        //creationBoutonChoix(3);
        //destructionBoutonChoix();
    }

    public void creationBoutonChoix(int nbChoix){
        int i = 0;
        float width = UI_choix_canvas.GetComponent<RectTransform>().rect.width;
        Vector3 monVecteur = new Vector3(0, UI_choix_canvas.transform.position.y, 0f);
        for (i = 0; i < nbChoix; i++)
        {
            monVecteur.x = (i * (width/(nbChoix - 1))) - width/2;
            GameObject newButton = Instantiate(UI_choix, monVecteur, Quaternion.identity, UI_choix_canvas.transform);
            newButton.GetComponent<RectTransform>().offsetMin = new Vector2(i*(width/nbChoix), 0);
            newButton.GetComponent<RectTransform>().offsetMax = new Vector2(-(nbChoix-(i+1)) * (width / nbChoix), 0);
            //ajout du numéro du bouton
            newButton.GetComponent<UI_choix>().numBouton = i;
            //Ajout du text aux boutons
            switch (i)
            {
                case 0: 
                    newButton.GetComponent<UI_choix>().Text = "Je ne sais pas quoi te dire !";
                    break;
                case 1:
                    newButton.GetComponent<UI_choix>().Text = "Non, je ne pense pas..";
                    break;
                case 2:
                    newButton.GetComponent<UI_choix>().Text = "Ne dis pas ça..";
                    break;
            }
            //ajout de la référence de la réponse aux boutons
            newButton.GetComponent<UI_choix>().Game_Manager = Game_Manager;

            //newButton.transform.Find("Text").GetComponent<Text>().text = width.ToString();

        }

    }

    public void destructionBoutonChoix()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}