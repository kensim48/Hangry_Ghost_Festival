using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDoor : MonoBehaviour
{
    private RoomTemplates templates;

	void Start(){

		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		templates.doors.Add(this.gameObject);
        gameObject.SetActive(false);
	}
}
