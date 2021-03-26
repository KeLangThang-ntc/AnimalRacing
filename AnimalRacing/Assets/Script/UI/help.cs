using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class help : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Help;
    public void checkedHelp()
    {
        if(gameObject.tag == "Help"){
            Help.SetActive(!Help.activeSelf);
            return;
        }
    }
}
