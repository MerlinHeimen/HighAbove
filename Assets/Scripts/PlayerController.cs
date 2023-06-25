using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement Speed & Jumpforce
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _jumpForce = 20;
    //[SerializeField] private float _doublejumpForce = 5;
    [SerializeField] private float _dashForce = 10000;
    
    private float _forceMultiplier = 1000;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private bool _GroundCheck;
    [SerializeField] private bool _doublejumpCheck;
    [SerializeField] private bool _DashCheck;

    public float GroundCheckDistance = 0.05f;
    public float downForce = 50;

    public bool wall;

    
    



    // Update is called once per frame
    void Update()
    {
        //Movement
        var vel = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * _speed;

        vel.y = _rb.velocity.y;
        _rb.velocity = vel;

        //Jump if Space is pressed & GroundCheck = true
        if (Input.GetKeyDown(KeyCode.Space) && _GroundCheck == true)
        {
            _rb.AddForce(Vector3.up * (_jumpForce * _forceMultiplier));
        }
            //Add down force 
        if (_rb.velocity.y < 0.0f && _GroundCheck == false)
        {
            _rb.AddForce(Vector3.down * downForce);

            if (_rb.velocity.y < (-20.0f))
            {
                _rb.AddForce(Vector3.up * (downForce * 2));
                Debug.Log("Canceling downForce: " + _rb.velocity);

            }
        }


        //Dash - adds an impulse force in whatever direction the player is moving on the horizontal axis
        var dashDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        
            //Pushing dashDirection to max value depending on input to get consistent dash distance
        if (Input.GetAxis("Horizontal") > 0.0f)
        {
            dashDirection = Vector3.right;
        }
        else if (Input.GetAxis("Horizontal") < 0.0f)
        {
            dashDirection = Vector3.left;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && _GroundCheck == false && _DashCheck == false)
        {
            _rb.AddForce(dashDirection * (_dashForce * _forceMultiplier), ForceMode.Impulse);
            //_rb.AddForce(dashDirection * (_dashForce * _forceMultiplier));
            _DashCheck = true;
            Debug.Log("Dash!");
        }

        //Double Jump
        if (Input.GetKeyDown(KeyCode.Space) && _GroundCheck == false && _doublejumpCheck == false) 
        {
            _rb.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            _rb.AddForce(Vector3.up * (_jumpForce * _forceMultiplier));
            _doublejumpCheck = true;
            //Debug.Log("WHEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE!");
        }

        //Disable movement in collider direction
        /*var movementRight = new Vector3(-1, 0, 0);
        if (wall == true)
        {
            if (wall == true && Input.GetAxis("Horizontal") > 0)
            {
                vel = vel - movementRight;
                Debug.Log("Input neutralized: " + vel);

            }
            else
            {
                _rb.velocity = vel;
            }
        }*/
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
            //Debug.Log("Am Boden");

        }
        else
        {
            _GroundCheck = false;
            //Debug.Log("In der Luft");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //var inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        //var inputDirectionInverted = new Vector3(-Input.GetAxis("Horizontal"), 0, 0) * _speed;
        if (other.gameObject.CompareTag("LeftCollider") || other.gameObject.CompareTag("RightCollider"))
        {
            wall = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        wall = false;
    }

}

