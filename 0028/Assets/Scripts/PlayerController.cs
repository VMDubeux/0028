using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Camera _cameraFPS;
    [SerializeField] float _range = 100.0f;
    [SerializeField] float _moveSpeed = 20f;
    [SerializeField] float _turnSpeed = 1f;
    [SerializeField] float _damage = 30f;
    float _xValue, _yValue, _zValue = 0.05f;

    void Start()
    {

    }

    void Update()
    {
        MovePlayer();
        FaceTarget();
        Fire();
    }

    void MovePlayer()
    {
        _xValue = Input.GetAxis("Horizontal") * -1 * Time.deltaTime * _moveSpeed;
        _yValue = Input.GetAxis("Vertical") * Time.deltaTime * _moveSpeed;
        transform.Translate(_xValue, _yValue, _zValue);
    }

    void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _turnSpeed);
    }

    void Fire()
    {
        if (Input.GetButton("Jump"))
            Shoot();
    }

    void Shoot()
    {
        RaycastHit _hit;
        if (Physics.Raycast(_cameraFPS.transform.position, _cameraFPS.transform.forward, out _hit, _range))
        {
            EnemyHealth _target = _hit.transform.GetComponent<EnemyHealth>();
            if (_target == null) return;
            _target.TakeDamage(_damage);
        }
        else return;
    }
}
