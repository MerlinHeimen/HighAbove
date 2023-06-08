using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement Speed & Jumpforce
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _jumpForce = 200;
    [SerializeField] private float _doublejumpForce = 5;
    [SerializeField] private float _dashForce = 100;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private bool _GroundCheck;
    [SerializeField] private bool _doublejumpCheck;
    [SerializeField] private bool _DashCheck;

    public float GroundCheckDistance = 0.05f;






    // Update is called once per frame
    void Update()
    {

        var vel = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * _speed;

        vel.y = _rb.velocity.y;
        _rb.velocity = vel;

        //Jump if Space is pressed & GroundCheck = true
        if (Input.GetKeyDown(KeyCode.Space) && _GroundCheck == true)
        {
            _rb.AddForce(Vector3.up * _jumpForce);
        }

        //Dash
        var dashDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        if (Input.GetKeyDown(KeyCode.LeftShift) && _GroundCheck == false && _DashCheck == false)
        {
            _rb.AddForce(dashDirection * _dashForce, ForceMode.Impulse);
            _DashCheck = true;
            Debug.Log("Dash!");
        }

        //Double Jump
        if (Input.GetKeyDown(KeyCode.Space) && _GroundCheck == false && _doublejumpCheck == false) 
        {
            _rb.AddForce(Vector3.up * _doublejumpForce, ForceMode.Impulse);
            _doublejumpCheck = true;
            Debug.Log("WHEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE!");
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
            _doublejumpCheck = false;
            _DashCheck = false;
            Debug.Log("Am Boden");

        }
        else
        {
            _GroundCheck = false;
            Debug.Log("In der Luft");
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

