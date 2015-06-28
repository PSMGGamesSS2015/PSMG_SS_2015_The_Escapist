using UnityEngine;
using System.Collections;

public class automaticDoorsTerminal : MonoBehaviour {

	public automaticDoors doors;

	void OnTriggerEnter( Collider other )
	{
		if( other.tag == "Player" )
		{
			doors.Open();
		}
	}

	void OnTriggerExit( Collider other )
	{
		if( other.tag == "Player" )
		{
			doors.Close();
		}
	}
}
