using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStairs : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			Debug.Log("Player tp boss");
            var pos =other.transform.position;
            other.transform.position = new Vector3(500,465,pos.z);
		}
	}
}
