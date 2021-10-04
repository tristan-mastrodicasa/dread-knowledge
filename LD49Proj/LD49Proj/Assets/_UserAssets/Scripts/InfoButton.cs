using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour
{

    public GameObject panel;
    public Text buttonText;
    public void Activate(){
        if (panel.activeInHierarchy){
            panel.SetActive(false);
            buttonText.text = "Info";
        }
        else{
            panel.SetActive(true);
            buttonText.text = "Close";
        }
    }
}
