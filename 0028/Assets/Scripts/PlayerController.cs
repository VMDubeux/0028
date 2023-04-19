using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float turnSpeed = 1f;
    float xValue, yValue, zValue = 0.05f;

    void Start()
    {

    }

    void Update()
    {
        MovePlayer();
        FaceTarget();
    }

    void MovePlayer()
    {
        xValue = Input.GetAxis("Horizontal") * -1 * Time.deltaTime * moveSpeed;
        yValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        transform.Translate(xValue, yValue, zValue);
    }

    void FaceTarget() 
    { 
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
}
