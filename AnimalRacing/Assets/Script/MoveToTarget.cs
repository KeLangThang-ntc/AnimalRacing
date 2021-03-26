using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject enemy;
   // public GameObject finish;
    //private bool checkFinished;
    NavMeshAgent meshAgent;
    void Start()
    {
        
        // checkFinished = true;
        meshAgent = gameObject.GetComponent<NavMeshAgent>();
    }
    private void CheckedFall()
    {
        if(enemy.transform.rotation.z > 0.4 || enemy.transform.rotation.z < -0.4 || enemy.transform.position.y > 210){
            StartCoroutine(ReSpeed(1.5f));
        }
            
    }
    IEnumerator ReSpeed(float seconds){
        meshAgent.speed = 0;
        yield return new WaitForSeconds(seconds);
        meshAgent.speed = 15;
    }

    // Update is called once per frame

    void Update()
    {
       // meshAgent.enabled = false;
        meshAgent.SetDestination(target.transform.position);
       
    }
    private void FixedUpdate() {
        CheckedFall();
    }
    
}
