using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeele : EnemyBase
{

    // Shooting
    public GameObject SwordPrefab;

    public void Start()
    {
        base.Start();
        speed++;
    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();   

    }

    

}
