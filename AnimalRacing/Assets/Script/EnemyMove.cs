using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float enemySpeed;
    private bool isColl;
    private Rigidbody rb;
    private void Start() {
        isColl = false;
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            isColl = true;
        }
        //isColl = false;
    }

    private void FixedUpdate() {
        //rb.MovePosition(rb.position+target.transform.position*enemySpeed*Time.fixedDeltaTime);
        transform.position = Vector3.Lerp(transform.position, target.transform.position, enemySpeed*Time.fixedDeltaTime);
        if(isColl == true){
            //print(isColl);
            transform.LookAt(null);
        }else{
           // print(isColl);
            transform.LookAt(target.transform);
        }
        FixedPostionTarget();
        checkFall();
    }


    private void checkFall(){
        if(transform.rotation.z > 0.4 || transform.rotation.z < -0.4 || transform.rotation.y > 0.3 || transform.rotation.y < -0.3 ){
            StartCoroutine(WaitStandUp(2f));
            //isColl = false;
        }
    }
    IEnumerator WaitStandUp(float seconds){
        yield return new WaitForSeconds(seconds);
        Quaternion delR = Quaternion.LookRotation(transform.forward, new Vector3(0,1,0));
        rb.MoveRotation(delR);
        isColl = false;
        //print(isColl);
    }


    private void FixedPostionTarget(){
        if(target.transform.position.z > transform.position.z + 10 ){
            target.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);
        }
        if(target.transform.position.z < transform.position.z - 10 ){
            target.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10);
        }
    }
}
