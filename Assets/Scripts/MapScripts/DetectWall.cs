using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWall : MonoBehaviour
{
    public GameObject Object;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Object.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
