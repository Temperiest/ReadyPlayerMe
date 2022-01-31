using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private GameObject target;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);
    }
}
