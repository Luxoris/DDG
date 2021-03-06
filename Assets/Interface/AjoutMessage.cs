﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class AjoutMessage : MonoBehaviour
{
    public GameObject UI_MessageEnvoye;
    public GameObject UI_MessageRecu;
    public GameObject UI_PrefabUpdateTime;
    public GameObject UI_ScrollBarVertical;
    public GameObject UI_Content;

    private float Pos_y;
    private float Tmp_Pos_y;

    // Start is called before the first frame update
    void Start()
    {
        Pos_y = 0;
        Tmp_Pos_y = 0;
        UI_Content.GetComponent<RectTransform>().anchoredPosition = new Vector3(UI_Content.GetComponent<RectTransform>().anchoredPosition.x, 0f);
    }


    public void AjoutMessageEnvoye(string Message, float offset_y=0f){
        float height;

        //ajout du message        
        Vector3 monVecteur = new Vector3(0, 0, 0);
        GameObject newMessage = Instantiate(UI_MessageEnvoye, monVecteur, Quaternion.identity, UI_Content.transform);
        RectTransform t = newMessage.GetComponent<RectTransform>();
        float posX = ((-t.offsetMin.x + t.offsetMax.x) * 0.5f);
        newMessage.GetComponentInChildren<Text>().text = Message.Replace("\n", "").Replace(((char)9).ToString(), "");
        
        //augmentation de la taille du message en fonction de la taille du text.
        height = LayoutUtility.GetPreferredHeight(newMessage.transform.GetChild(0).GetComponent<RectTransform>());
        height += 16;
        

        newMessage.GetComponent<RectTransform>().sizeDelta = new Vector2(newMessage.GetComponent<RectTransform>().sizeDelta.x, height);

  
        //augmente la taille du content
        this.AgrandirContent(((height + 8)));

        //incrémentation de la taille pos Y de l'instance

        this.Pos_y += this.Tmp_Pos_y + ((height + 8) / 2) + offset_y;
        this.Tmp_Pos_y = ((height + 8) / 2);

        //modifie position message
        newMessage.GetComponent<RectTransform>().anchoredPosition = new Vector3(posX, this.Pos_y, 0);

    }

    public void AjoutMessageRecu(string Message, float offset_y=0f)
    {
        float height;
        

        //ajout du message        
        Vector3 monVecteur = new Vector3(0, 0, 0);
        GameObject newMessage = Instantiate(UI_MessageRecu, monVecteur, Quaternion.identity, UI_Content.transform);
        RectTransform t = newMessage.GetComponent<RectTransform>();
        float posX = ((+t.offsetMin.x - t.offsetMax.x) * 0.5f);

        newMessage.GetComponentInChildren<Text>().text = Message.Replace("\n", "").Replace(((char)9).ToString(), "");


        //augmentation de la taille du message en fonction de la taille du text.
        height = LayoutUtility.GetPreferredHeight(newMessage.transform.GetChild(0).GetComponent<RectTransform>());
        height += 16;
        newMessage.GetComponent<RectTransform>().sizeDelta = new Vector2(newMessage.GetComponent<RectTransform>().sizeDelta.x, height);

        

        //augmente la taille du content
        this.AgrandirContent(((height + 8)));

        //incrémentation de la taille pos Y de l'instance
        this.Pos_y += this.Tmp_Pos_y + ((height + 8 ) / 2) + offset_y;
        this.Tmp_Pos_y = ((height + 8) / 2);

        //modifie position message
        newMessage.GetComponent<RectTransform>().anchoredPosition = new Vector3(posX, this.Pos_y, 0);
    }

    public void AjoutDate(string date)
    {
        float height;


        //ajout du message        
        Vector3 monVecteur = new Vector3(0, 0, 0);
        GameObject newMessage = Instantiate(UI_PrefabUpdateTime, monVecteur, Quaternion.identity, UI_Content.transform);
        RectTransform t = newMessage.GetComponent<RectTransform>();
        float posX = ((+t.offsetMin.x - t.offsetMax.x) * 0.5f);

        newMessage.GetComponentInChildren<TextMeshProUGUI>().text = date.Replace("_", " ");


        //augmentation de la taille du message en fonction de la taille du text.
        height = newMessage.GetComponentInChildren<TextMeshProUGUI>().fontSize;
        height += 2;
        newMessage.GetComponent<RectTransform>().sizeDelta = new Vector2(newMessage.GetComponent<RectTransform>().sizeDelta.x, height);



        //augmente la taille du content
        this.AgrandirContent(((height + 0)));

        //incrémentation de la taille pos Y de l'instance
        this.Pos_y += this.Tmp_Pos_y + ((height + 0) * 0.5f);
        this.Tmp_Pos_y = ((height + 0) *0.5f);

        //modifie position message
        newMessage.GetComponent<RectTransform>().anchoredPosition = new Vector3(posX, this.Pos_y, 0);
    }

    public void AgrandirContent(float valeur)
    {
        UI_Content.GetComponent<RectTransform>().sizeDelta = new Vector2(UI_Content.GetComponent<RectTransform>().sizeDelta.x, UI_Content.GetComponent<RectTransform>().sizeDelta.y+valeur);
        //UI_Content.GetComponent<RectTransform>().anchoredPosition = new Vector3(UI_Content.GetComponent<RectTransform>().anchoredPosition.x, UI_Content.GetComponent<RectTransform>().anchoredPosition.y);
    }
}
