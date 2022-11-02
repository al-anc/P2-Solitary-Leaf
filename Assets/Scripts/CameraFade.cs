using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFade : MonoBehaviour
{
    public GameObject Camera;
    public GameObject Player;
    [SerializeField]
    private LayerMask LayerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        Debug.DrawLine(transform.position, Player.transform.position + new Vector3(0,0,-0.3f), Color.green);
        if(Physics.Raycast(transform.position, fwd, 10, LayerMask))
        {
            Debug.Log("Hit Building");
        }
    }
}
