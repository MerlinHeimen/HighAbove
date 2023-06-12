using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    //Fetch From and To rotation
    public Transform from;
    public Transform to;

    [SerializeField] private float t = 0.0f;

    private float BridgeTimer = 0.0f;
    private float lerpTimer = 0.0f;

    public bool bridgeTriggerState = false;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Continue work on Bridge Controller. Bridge needs lerp for raise movement");
        //Setting rotation range
        transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, (t * 8));

        //Lowers bridge by adding deltaTime to t every frame
        if(bridgeTriggerState == true)
        {
            lerpTimer += Time.deltaTime;
            t = lerpTimer;
        }

        //Checks if the Bridge is raised or lowered and starts BridgeTimer if Bridge is lowered. Raises Bridge after time has run out.

        if (lerpTimer >= 1.0f)
        {
            bridgeTriggerState = false;
            lerpTimer = 0;
        }


        //TO WORK ON
        if (bridgeTriggerState == false){
            
            BridgeTimer += Time.deltaTime;

            if (BridgeTimer >= 1.0f)
            {
                t = 0.0f;
                BridgeTimer = 0.0f;
            }

            //Debug.Log(BridgeTimer);
         }


    }

    //Lowers Bridge on collision
    private void OnTriggerEnter(Collider other)
    {
        bridgeTriggerState = true;
    }
}
