using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrandSpawner : MonoBehaviour
{
    public GameObject noodleStrand;
    public Transform spawnNoodle;
    public GameObject playerParent;
    private bool ifCollide = false;
    void Start()
    {
        if (!ifCollide)
            Instantiate(noodleStrand, spawnNoodle.position, spawnNoodle.rotation, this.transform);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ifCollide = true;
    }
}
