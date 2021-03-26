using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 offset;
    public Transform target;
    public float translateSpeed;
    public float rotationSpeed;
    private void FixedUpdate() {
        CamTranslation();
        CamRotation();
    }
    private void CamTranslation(){
        Vector3 targetPositon = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPositon, translateSpeed*Time.fixedDeltaTime);
    }
    private void CamRotation(){
        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed*Time.fixedDeltaTime);
    }
}
