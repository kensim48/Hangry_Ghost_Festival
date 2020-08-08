using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDoor : MonoBehaviour
{
    private RoomTemplates templates;

	public Vector3 trans;

	void OnTriggerEnter2D(Collider2D other){
		// if(other.CompareTag("Wall"));
		// else 
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		if(templates.doors.Contains(transform.parent.gameObject) == false){
			if(other.gameObject.tag == "Door" || transform.parent.parent.parent.parent.GetComponent<AddRoom>().roomNumber<=5){
				// templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
				templates.doors.Add(transform.parent.gameObject);
				transform.parent.gameObject.SetActive(false);
				trans = transform.position;
				// Debug.Log("Test");
			}
			else
			{	
				templates.test ++;
				Debug.Log(other.gameObject.tag);
				Debug.Log(transform.parent.parent.parent.parent.gameObject.name);
			}
		}
	}
}
