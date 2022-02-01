using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform target;
    private Vector3 offset;
    private bool canTurn = false;
    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(canTurn) 
            transform.LookAt(new Vector3(target.position.x, transform.position.y,target.position.z));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canTurn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canTurn = false;
        }
    }
}
