using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] GameObject Player;
    
    void Update()
    {
        if (!Player.GetComponent<PlayerMovement>().hasDelivery)
        {
            //Target = GameObject.FindGameObjectWithTag("PickupPoint");
        }
        else if(Player.GetComponent<PlayerMovement>().hasDelivery)
        {
            //Target = GameObject.FindGameObjectWithTag("DropoffPoint");
        }
        else
        {
            //Target = GameObject.FindGameObjectWithTag("PickupPoint");
        }
        var direction = Target.position - transform.position;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
