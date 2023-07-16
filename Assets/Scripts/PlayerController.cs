using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement Speed & forces
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private float _jumpForce = 20.0f;
    //[SerializeField] private float _doublejumpForce = 5.0f;
    [SerializeField] private float _dashForce = 10.0f;
    private float _knockbackforce = 3.0f;
    private float _knockbackTimer;
    private bool _knockbackState;

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

            //Limit fall-speed
            if (_rb.velocity.y < (-20.0f))
            {
                _rb.AddForce(Vector3.up * (downForce * 2));
                Debug.Log("Canceling downForce: " + _rb.velocity);

            }
            /*else if (_rb.velocity.y > (2.0f))
            {
                _rb.AddForce(Vector3.down * (downForce * 2));
                Debug.Log("Canceling upForce: " + _rb.velocity);
                Debug.Log("Up velocity: " + _rb.velocity.y);


            }*/
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
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

        //Knockback
        if (_knockbackState == true)
        {
            _knockbackTimer += Time.deltaTime;
            _speed = 0.0f;

            if (_knockbackTimer >= 0.5f)
            {
                _knockbackTimer = 0f;
                _speed = 3.0f;
                _knockbackState = false;
            }
        }

        //Disable movement in collider direction --------------------------------------------------------------------------------------------
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
        //-------------------------------------------------------------
        //Knockback
        if (other.gameObject.CompareTag("Enemy"))
        {
            Knockback();
            _knockbackState = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //wall = false;
    }

    public void Knockback()
    {
        var knockbackToLeft = new Vector3(-1.0f, 0.0f, 0.0f);
        var knockbackToRight = new Vector3(1.0f, 0.0f, 0.0f);

        if (Input.GetAxis("Horizontal") >= 0.0f)
        {
            //_rb.AddForce((-knockbackDirection) * (_knockbackforce * _forceMultiplier), ForceMode.Impulse);
            _rb.AddForce(knockbackToLeft * (_knockbackforce * _forceMultiplier), ForceMode.Impulse);
        }
        
        if (Input.GetAxis("Horizontal") < 0.0f)
        {
            _rb.AddForce(knockbackToRight * (_knockbackforce * _forceMultiplier), ForceMode.Impulse);
        }
    }

}

