using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDoor : MonoBehaviour
{
    private RoomTemplates templates;

	public Vector3 trans;

	void Start(){
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		templates.doors.Add(this.gameObject);
        gameObject.SetActive(false);
		trans = transform.position;
	}
}
