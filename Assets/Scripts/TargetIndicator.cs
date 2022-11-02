using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] Transform Pickup;
    [SerializeField] Transform Dropoff;
    [SerializeField] GameObject Player;
    [SerializeField] float HideDistance;
    
    void Update()
    {
        if (!Player.GetComponent<PlayerMovement>().hasDelivery)
        {
            Pickup = GameObject.FindWithTag("PickupPoint").transform;
            Target = Pickup;
            Debug.Log("NoDeliveryCheck");
        }
        else if (Player.GetComponent<PlayerMovement>().hasDelivery)
        {
            Dropoff = GameObject.FindWithTag("DropoffPoint").transform;
            Target = Dropoff;
            Debug.Log("HasDeliveryCheck");
        }
        else
        {
            Pickup = GameObject.FindWithTag("PickupPoint").transform;
            Target = Pickup;
        }
        var direction = Target.position - transform.position;

        if (direction.magnitude < HideDistance)
        {
            SetChildrenActive(false);

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            SetChildrenActive(true);
            
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void SetChildrenActive(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }
}
