using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    //Fetch From and To rotation
    public Transform from;
    public Transform to;

    public float rotationSpeed = 8;

    [SerializeField] private float t = 0.0f;

    private float BridgeTimer = 0.0f;
    private float lerpTimer = 0.0f;

    public bool bridgeTrigger = false;
    public bool bridgeStateUp = true;
    public bool bridgeStateDown = false;

    // Update is called once per frame
    void Update()
    {
        //Setting rotation range
        transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, t);

        //Lowers bridge by adding deltaTime to t every frame and setting bridgeStateDown = true if bridge is fully lowered
        if(bridgeStateUp == true && bridgeTrigger == true)
        {
            lerpTimer += (Time.deltaTime * rotationSpeed);
            t = lerpTimer;
            //Debug.Log(lerpTimer);
        }

        //Sets bridge states and resets timers if bridge arrives at lowered state
        if (t >= 1.0f)
        {
            bridgeStateDown = true;
            bridgeStateUp = false;
            bridgeTrigger = false;
            t = 1.0f;
        }

        //Checks if the Bridge is in lowered state and starts countdown to raise bridge. Raises bridge after countdonw runs out [1 sec]

        if (bridgeStateDown == true)
        {

            BridgeTimer += Time.deltaTime;

            if (BridgeTimer >= 1.0f && lerpTimer >= 0)
            {
                lerpTimer -= (Time.deltaTime * rotationSpeed);
                t = lerpTimer;
            }
        }

        //Sets bridge states and resets timers if bridge arrives at raised state
        if (t <= 0.0f)
        {
            bridgeStateDown = false;
            bridgeStateUp = true;
            BridgeTimer = 0.0f;
            t = 0.0f;
        }

    }

    //Lowers Bridge on collision
    private void OnTriggerEnter(Collider other)
    {
        bridgeTrigger = true;
    }
}
