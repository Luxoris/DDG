using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AjoutMessage : MonoBehaviour
{
    public GameObject UI_MessageEnvoye;
    public GameObject UI_MessageRecu;
    public GameObject UI_ScrollBarVertical;
    public GameObject UI_Content;

    public float Pos_y = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }


    public void AjoutMessageEnvoye(string Message){
        float height = 30;

        //ajout du message        
        Vector3 monVecteur = new Vector3(0, 0, 0);
        GameObject newMessage = Instantiate(UI_MessageEnvoye, monVecteur, Quaternion.identity, UI_Content.transform);
        newMessage.GetComponent<Text>().text = Message;

        //augmente la taille du content
        this.AgrandirContent(height);
        //incrémentation de la taille pos Y de l'instance
        this.Pos_y += height;


        //modifie position message
        newMessage.GetComponent<RectTransform>().anchoredPosition = new Vector3(newMessage.GetComponent<RectTransform>().anchoredPosition.x, this.Pos_y, 0);
        

    }

    public void AjoutMessageRecu(string Message)
    {
        float height = 30;

        //ajout du message        
        Vector3 monVecteur = new Vector3(0, 0, 0);
        GameObject newMessage = Instantiate(UI_MessageRecu, monVecteur, Quaternion.identity, UI_Content.transform);
        newMessage.GetComponent<Text>().text = Message;

        //augmente la taille du content
        this.AgrandirContent(height);
        //incrémentation de la taille pos Y de l'instance
        this.Pos_y += height;

        //modifie position message
        newMessage.GetComponent<RectTransform>().anchoredPosition = new Vector3(newMessage.GetComponent<RectTransform>().anchoredPosition.x, this.Pos_y, 0);
    }

    public void AgrandirContent(float valeur)
    {
        UI_Content.GetComponent<RectTransform>().sizeDelta = new Vector2(UI_Content.GetComponent<RectTransform>().sizeDelta.x, UI_Content.GetComponent<RectTransform>().sizeDelta.y+valeur);
        UI_Content.GetComponent<RectTransform>().anchoredPosition = new Vector3(UI_Content.GetComponent<RectTransform>().anchoredPosition.x, UI_Content.GetComponent<RectTransform>().anchoredPosition.y - valeur);
    }
}
