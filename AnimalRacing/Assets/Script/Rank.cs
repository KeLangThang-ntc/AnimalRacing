using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rank : MonoBehaviour
{

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            print("You Win!");
        }else if(other.gameObject.tag == "Enemy"){
            print("You lose!");
        }  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
