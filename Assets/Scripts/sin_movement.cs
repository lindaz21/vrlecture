using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sin_movement : MonoBehaviour
{
    private void awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += new Vector3(0.5f * Time.deltaTime, 0, 0);
    }
}
