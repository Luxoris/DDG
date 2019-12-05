using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_choix : MonoBehaviour
{
    public GameObject UI_Reponse;
    public string Text;
    public string Choix_Text;

    public void UpdateReponse()
    {
        UI_Reponse.GetComponent<Text>().text = this.Text;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
