﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{
    public Text Timetext;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timetext.text = System.DateTime.Now.ToString("HH:mm");
    }
}
