using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patroling : MonoBehaviour {
	//este es para poner a un animal curioso que siga unos puntos de control
	public Transform player;
	public Animator anim;
	//static Animator anim;
	public Transform head;
	string state = "patrol";
	public GameObject[] wayPoints;
	int currentWP =0;
	float rotSpeed = 0.8f;
	float speed = 2.8f;
	float acuraciWP = 3.0f;


	// Use this for initialization
	void Start () {
		//agrega las animaciones bool, ESTAS SE HACEN CON OPCION BOOL EN LOS PARAMETROS DEL"ANIMATOR" Y SE AGREGAN LOS NOMBRES.
		anim = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 direction = player.position - this.transform.position;
		direction.y = 0;
		float angle = Vector3.Angle (direction, head.up);
		if (state == "patrol" && wayPoints.Length > 0) 
		{
			anim.SetBool ("animIdle" , false);
			anim.SetBool ("animRun", true);
			if (Vector3.Distance (wayPoints [currentWP].transform.position, transform.position) < acuraciWP) 
			{
				//anim.SetBool ("animIdle" , true);
				//anim.SetBool ("animRun", false);
				currentWP++;
				if (currentWP >= wayPoints.Length) 
				{
					currentWP = 0;
				}
			}
			//rotate towards waypoints
			direction = wayPoints[currentWP].transform.position - transform.position;
			this.transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), rotSpeed * Time.deltaTime);
			this.transform.Translate (0, 0, Time.deltaTime * speed);
			direction.y = 1;
		}
		 
		if (Vector3.Distance ( player.position, this.transform.position) < 15 && (angle <60 || state == "pursuing"))
		{
			state = "pursuing";
			this.transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), rotSpeed * Time.deltaTime);
			//distancia a la que vendra el conejo
			if (direction.magnitude >4)
			{
				this.transform.Translate (0, 0, Time.deltaTime * speed);
				anim.SetBool("animRun", true);
				anim.SetBool("animIdle",false);
			}
			else
			{
				anim.SetBool("animRun", false);
				anim.SetBool("animIdle",true);
			}
		}
		else
		{
			anim.SetBool("animRun", true);
			anim.SetBool("animIdle",false);
			state = "patrol";
		}
			

	}

}
