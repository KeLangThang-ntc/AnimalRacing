using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rhinoceros : MonoBehaviour
{
    private bool skill;
    private bool useSkill;
    private Rigidbody rb;
    private Animator animRhino;
    private int power;
    public GameObject par;
    
    // Start is called before the first frame update
    void Start()
    {
        power = 0;
        skill = false;
        useSkill = false;
        rb = gameObject.GetComponent<Rigidbody>();
        animRhino = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision other) {
        if(useSkill == true && other.gameObject.tag == "Enemy"){
            other.transform.position = other.transform.position + new Vector3(0,10,0);
            useSkill = false;
        }
        if(other.gameObject.tag == "power"){
            power = 10;
            Destroy(other.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(power == 10){
            par.SetActive(true);
            if(Input.GetKey(KeyCode.Space)){
            skill = true;
            useSkill = true;
        }
        }
    }
    private void FixedUpdate() {
        if(skill == true && useSkill == true){
            StartCoroutine(WaitSkill(0.2f));
        }
    }
    IEnumerator WaitSkill(float seconds){
        animRhino.SetBool("isAttack", true);
        yield return new WaitForSeconds(seconds);
        rb.MovePosition(rb.position + transform.forward*20*Time.fixedDeltaTime);
        yield return new WaitForSeconds(seconds);
        animRhino.SetBool("isAttack",false);
        useSkill = false;
        skill = false;
        power = 0;
        par.SetActive(false);
    }
}
