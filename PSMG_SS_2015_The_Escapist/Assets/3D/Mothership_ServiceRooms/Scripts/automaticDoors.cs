using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class automaticDoors : MonoBehaviour {

	public float openSpeed = 5f;

	public List< Light > lights = new List<Light>();

	public Color openedColor = new Color( 1f, 1f, 1f, 1f );
	public Color closedColor = new Color( 0f, 0f, 0f, 0f );

	public Transform doorsWing;
	public Vector3 openedPosition = new Vector3( 0f, 0f, 0f );
	private Vector3 closedPosition = new Vector3( 0f, 0f, 0f );

	private int state = 0; // 0 - closed, 1 - opening, 2 - opened, 3 - closing
	private float lerper = 0f;

	public bool openable = true;

	public AudioClip openSFX;
	public AudioClip closeSFX;

	void Start()
	{
		closedPosition = doorsWing.transform.localPosition;

		if( openable )
		{
			foreach( Light l in lights ) l.color = openedColor;
		}
		else if( !openable )
		{
			foreach( Light l in lights ) l.color = closedColor;
		}
	}

	void Update()
	{
		if( state == 0 )
		{
			//do nothing
		}
		else if( state == 1 )
		{
			//opening doors
			if( lerper < 1f )
			{
				lerper += Time.deltaTime * openSpeed;
				doorsWing.localPosition = Vector3.Lerp( closedPosition, openedPosition, lerper );
			}
			else
			{
				state = 2;
				return;
			}
		}
		else if( state == 2 )
		{
			//do noting
		}
		else if( state == 3 )
		{
			//closing doors

			if( lerper > 0f )
			{
				lerper -= Time.deltaTime * openSpeed;
				doorsWing.localPosition = Vector3.Lerp( closedPosition, openedPosition, lerper );
			}
			else
			{
				state = 0;
				return;
			}
		}
	}

	public void Open()
	{
		if( !openable ) return;

		//lerper = 0f;
		state = 1;

		if( openSFX ) GetComponent<AudioSource>().PlayOneShot( openSFX );
	}

	public void Close()
	{
		if( !openable ) return;

		//lerper = 1f;
		state = 3;

		if( closeSFX ) GetComponent<AudioSource>().PlayOneShot( closeSFX );
	}

	public void setOpenable( bool o )
	{
		openable = o;

		if( openable )
		{
			foreach( Light l in lights ) l.color = openedColor;
		}
		else if( !openable )
		{
			foreach( Light l in lights ) l.color = closedColor;
		}
	}
}
