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

    // Update is called once per frame
    void Update()
    {
        //Setting rotation range
        transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, t);

        //Checks the status of Bridge and BridgeTimer. Resets Bridge after time has run out.
        if (t == 1.0f){
            
            BridgeTimer = BridgeTimer + Time.deltaTime;

            if (BridgeTimer >= 1.0f)
            {
                t = 0.0f;
                BridgeTimer = 0.0f;
            }
         }
    }

    //Lowers Bridge on collision (reacts to wrench-collider only in the future)
    private void OnCollisionEnter(Collision collision)
    {
        if(t == 0.0f){
            t = 1.0f;
        }
    }
}
