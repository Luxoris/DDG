using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_choix : MonoBehaviour
{
    public GameObject Game_Manager;
    public string Text;
    public string Choix_Text;
    public int numBouton;

    public void UpdateReponse()
    {
        Game_Manager.GetComponent<Game.Game>().Next(numBouton);
    }

    // Start is called before the first frame update
    void Start()
    {
        //this.GetComponent<Text>().text = this.Text;
        this.GetComponentInChildren<Text>().text = this.Text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
