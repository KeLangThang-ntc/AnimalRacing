using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class move : MonoBehaviour
{
    private float playerHeight = 2f;
    private Rigidbody rb;
    [Header("Move")]
    [SerializeField] private float moveSpeed = 10f;
    private float horizotal;
    private float vertical;
    private Vector3 moveDirection;

    [Header("groundDetection")]
    private bool isGrounded;
    float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float rotationSpeed = 1f;
    RaycastHit slopeHit;
    private Vector3 slopeMoveDirection;
    private Animator anim;

    private bool onSlope(){
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight/2 + 2f)){
            if(slopeHit.normal != Vector3.up){
                return true;
            }else{
                return false;
            }
        }
        return false;
    }

    private void Start() {
        transform.tag = "Player";
        // NavMeshObstacle navo = gameObject.GetComponent<NavMeshObstacle>();
        // navo.enabled = true;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //rb.freezeRotation = true;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Update() {
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0,1,0), groundDistance, groundMask);
        gameInput();
        checkFall();
        Inertia();
        
    }
    private void Inertia(){
        if (Input.GetButtonUp("Vertical") && isGrounded)
        {
            rb.AddForce(Vector3.forward*moveSpeed*10f, ForceMode.Acceleration);
        }
    }
    private void checkFall(){
        if(transform.rotation.z > 0.4 || transform.rotation.z < -0.4 || transform.position.y > 210){
            moveSpeed = 0;
            StartCoroutine(WaitStandUp(1.5f));
        }else{
            moveSpeed = 15;
        }
        if(Input.GetKey(KeyCode.RightControl)){
            StartCoroutine(WaitStandUp(0.5f)); // StandUp
        }
    }
    IEnumerator WaitStandUp(float seconds){
        yield return new WaitForSeconds(seconds);
        Quaternion delR = Quaternion.LookRotation(transform.forward, new Vector3(0,1,0));
        rb.MoveRotation(delR);
    }
    
    private void gameInput(){
        horizotal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        moveDirection = transform.forward*vertical;
        if(vertical != 0){
            anim.SetBool("isRun", true);
        }else{
            anim.SetBool("isRun", false);
        }
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void FixedUpdate() {
        MovePlayer();
        Drift();
        RotatePlayer();
    }
    private void Drift(){
        if(Input.GetKey(KeyCode.RightShift)){
            rotationSpeed = 2;
        }else{
            rotationSpeed = 1;
        }
    }
    private void RotatePlayer(){
        if(horizotal > 0){       
            Quaternion delR = Quaternion.Euler(0, rotationSpeed ,0);
            rb.MoveRotation(rb.rotation*delR);
        }else if(horizotal < 0){
            Quaternion delR = Quaternion.Euler(0, -rotationSpeed ,0);
            rb.MoveRotation(rb.rotation*delR);
        }
    }
    private void MovePlayer(){
        if(isGrounded && !onSlope()){
            //rb.AddForce(moveDirection.normalized*moveSpeed*2f, ForceMode.Acceleration);
            rb.MovePosition(rb.position+moveDirection*moveSpeed*Time.fixedDeltaTime);
        }else if(isGrounded && onSlope()){
            //rb.AddForce(slopeMoveDirection.normalized*moveSpeed*2f, ForceMode.Acceleration);
            rb.MovePosition(rb.position+slopeMoveDirection*moveSpeed*Time.fixedDeltaTime);
        }
       
    }
}
