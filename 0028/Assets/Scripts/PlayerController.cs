using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Camera _cameraFPS;
    Rigidbody _rgbodyPlayer;
    RaycastHit _hit;
    Vector3 _direction;
    public LayerMask _layerMask;
    [SerializeField] float _range = 100.0f;
    [SerializeField] float _moveSpeed = 20f;
    [SerializeField] float _turnSpeed = 1f;
    [SerializeField] float _damage = 30f;
    float _xValue, _yValue, _zValue = 0.05f;

    void Start()
    {
        _rgbodyPlayer = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //FaceTarget();
        Fire();
    }

    void FixedUpdate()
    {
        MovePlayer();
        RotationAimPosition();
    }

    /*void FaceTarget()
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(_direction.x, 0, _direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _turnSpeed);
    }*/

    void Fire()
    {
        if (Input.GetButton("Jump"))
            Shoot();
    }

    void MovePlayer()
    {
        _xValue = Input.GetAxis("Horizontal") * -1 * Time.deltaTime * _moveSpeed;
        _yValue = Input.GetAxis("Vertical") * Time.deltaTime * _moveSpeed;
        transform.Translate(_xValue, _yValue, _zValue);
        RigidbodyMovePlayer();
    }

    void RotationAimPosition()
    {
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(_cameraFPS.transform.position, _cameraFPS.transform.forward * 100, Color.cyan);
        _direction = (_target.position - transform.position).normalized;
        if (Physics.Raycast(raio, out _hit, _range, _layerMask))
        {
            Vector3 _aimPlayerPosition = _hit.point - transform.position;
            Quaternion _newRotation = Quaternion.LookRotation(_aimPlayerPosition);
            _rgbodyPlayer.MoveRotation(_newRotation);
        }
    }

    void RigidbodyMovePlayer()
    {
        _rgbodyPlayer.MovePosition
                   (_rgbodyPlayer.position +
                   (_direction * _moveSpeed * Time.deltaTime));
    }

    void Shoot()
    {
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(raio, out _hit, _range, _layerMask))
        {
            EnemyHealth _target = _hit.transform.GetComponent<EnemyHealth>();
            if (_target == null) return;
            _target.TakeDamage(_damage);
        }
        else return;
    }
}

