using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class CreateButton : MonoBehaviour
{
    public GameObject UI_choix;
    public GameObject UI_choix_canvas;
    public GameObject Game_Manager;
    public GameObject UI_ScrollViewMessage;
    public GameObject UI_BoutonEnvoye;
    public List<GameObject> listeBouton = new List<GameObject>();

    private int MARGE = 2;
    private int PADDING = 4;

    void Start()
    {
        //creationBoutonChoix(3);
        //destructionBoutonChoix();
    }

    public void creationBoutonChoix(int nbChoix, Reponse[] r)
    {
        if (nbChoix > 1)
        {
            int i = 0;
            float height = 0;

            Vector3 monVecteur = new Vector3(UI_choix_canvas.transform.position.x, 0, 0f);
            float Tmp_Pos_y = 15;
            float posY = 0;

            GameObject newButton = Instantiate(UI_choix, monVecteur, Quaternion.identity, UI_choix_canvas.transform);
            RectTransform t = newButton.GetComponent<RectTransform>();
            newButton.transform.GetChild(0).GetComponent<Text>().text = r[i].TxtReponse.Replace("\n", "").Replace(((char)9).ToString(), "");

            float posX = ((+t.offsetMin.x - t.offsetMax.x) * 0.5f);
            //newButton.GetComponent<RectTransform>().offsetMax = new Vector2(0, -(i) * (height* 0.75f) -5);
            //newButton.GetComponent<RectTransform>().offsetMin = new Vector2(0, ((nbChoix - i -1f) * (height*0.75f)) +5);
            height = LayoutUtility.GetPreferredHeight(newButton.transform.GetChild(0).GetComponent<RectTransform>()) + PADDING;
            newButton.GetComponent<RectTransform>().sizeDelta = new Vector2(newButton.GetComponent<RectTransform>().sizeDelta.x, height);
            this.AgrandirContent(((height + MARGE)));
            Tmp_Pos_y = nbChoix;

            posY += Tmp_Pos_y + ((height + MARGE) / 2);
            Tmp_Pos_y = ((height + MARGE) / 2);
            newButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, posY);

            //ajout du numéro du bouton
            newButton.GetComponent<UI_choix>().numBouton = i;
            //ajout de la référence de la réponse aux boutons
            newButton.GetComponent<UI_choix>().Game_Manager = Game_Manager;

            listeBouton.Add(newButton);

            //newButton.transform.Find("Text").GetComponent<Text>().text = width.ToString();
            for (i = 1; i < nbChoix; i++)
            {

                newButton = Instantiate(UI_choix, monVecteur, Quaternion.identity, UI_choix_canvas.transform);
                t = newButton.GetComponent<RectTransform>();
                newButton.transform.GetChild(0).GetComponent<Text>().text = r[i].TxtReponse.Replace("\n", "").Replace(((char)9).ToString(), "");
                posX = ((+t.offsetMin.x - t.offsetMax.x) * 0.5f);
                //newButton.GetComponent<RectTransform>().offsetMax = new Vector2(0, -(i) * (height* 0.75f) -5);
                //newButton.GetComponent<RectTransform>().offsetMin = new Vector2(0, ((nbChoix - i -1f) * (height*0.75f)) +5);
                height = LayoutUtility.GetPreferredHeight(newButton.transform.GetChild(0).GetComponent<RectTransform>()) + PADDING;
                newButton.GetComponent<RectTransform>().sizeDelta = new Vector2(newButton.GetComponent<RectTransform>().sizeDelta.x, height);
                this.AgrandirContent(((height + MARGE)));

                posY += Tmp_Pos_y + ((height + MARGE) / 2);
                Tmp_Pos_y = ((height + MARGE) / 2);
                newButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, posY);

                //ajout du numéro du bouton
                newButton.GetComponent<UI_choix>().numBouton = i;
                //ajout de la référence de la réponse aux boutons
                newButton.GetComponent<UI_choix>().Game_Manager = Game_Manager;

                listeBouton.Add(newButton);
                
                //newButton.transform.Find("Text").GetComponent<Text>().text = width.ToString();

            }
            //UI_choix_canvas.GetComponent<RectTransform>().offsetMax = new Vector2(UI_choix_canvas.GetComponent<RectTransform>().offsetMax.x, ((height) * (nbChoix + 1)));
            //UI_ScrollViewMessage.GetComponent<RectTransform>().offsetMin = new Vector2(UI_ScrollViewMessage.GetComponent<RectTransform>().offsetMin.x, ((height) * (nbChoix + 1)));
        }
        else
        {
            Game_Manager.GetComponent<Game.Game>().NumReponseSelectionne = 0;
            UI_BoutonEnvoye.GetComponent<Button>().Select();
        }

    }

    public void AgrandirContent(float valeur)
    {
        UI_choix_canvas.GetComponent<RectTransform>().offsetMax = new Vector2(UI_choix_canvas.GetComponent<RectTransform>().offsetMax.x, UI_choix_canvas.GetComponent<RectTransform>().offsetMax.y + valeur);
        UI_ScrollViewMessage.GetComponent<RectTransform>().offsetMin = new Vector2(UI_ScrollViewMessage.GetComponent<RectTransform>().offsetMin.x, UI_ScrollViewMessage.GetComponent<RectTransform>().offsetMin.y + valeur);
    }

    public void destructionBoutonChoix()
    {
        foreach (GameObject bouton in listeBouton)
        {
            Destroy(bouton);
        }
        listeBouton.Clear();
        UI_choix_canvas.GetComponent<RectTransform>().offsetMax = new Vector2(UI_choix_canvas.GetComponent<RectTransform>().offsetMax.x, (40));
        UI_ScrollViewMessage.GetComponent<RectTransform>().offsetMin = new Vector2(UI_ScrollViewMessage.GetComponent<RectTransform>().offsetMin.x, (40));

    }
}