using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement Speed & Jumpforce
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _jumpForce = 200;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private bool _GroundCheck;
    
    public float GroundCheckDistance = 0.1f;
    public float GroundBuffer = 0.1f;

    




    // Update is called once per frame
    void Update()
    {
        var vel = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _speed;

        vel.y = _rb.velocity.y;
        _rb.velocity = vel;

        //Jump if Space is pressed & GroundCheck = true
        if (Input.GetKeyDown(KeyCode.Space) && _GroundCheck == true)
        {
            _rb.AddForce(Vector3.up * _jumpForce);
        }
        

    }

    void FixedUpdate()
    {
        //GroundCheckDistance = (GetComponent<CapsuleCollider>().height / 2) + GroundBuffer;
        Debug.DrawRay(transform.position, -transform.up, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, GroundCheckDistance))
        {
            _GroundCheck = true;
            Debug.Log("Geht nicht");
            
        }
        else
        {
            _GroundCheck = false;
            Debug.Log("Geht auch nicht");
        }
    }



    //Old Groundcheck
    /*private void OnCollisionEnter(Collision collision)
    {
        _GroundCheck = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        _GroundCheck = false;
    }*/

}

