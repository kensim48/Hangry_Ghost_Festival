using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStairs : MonoBehaviour
{
	public GameObject shopRoom;
    void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			Debug.Log("Player tp boss");
			shopRoom = GameObject.FindWithTag("Shop");
            var pos =other.transform.position;
			var pos1 = shopRoom.transform.position;
            other.transform.position = new Vector3(pos1.x,pos1.y,pos.z);
		}
	}
}
