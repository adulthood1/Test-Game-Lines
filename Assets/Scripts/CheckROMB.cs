using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckROMB : MonoBehaviour {
	
	Ray ray;
	RaycastHit hit;
	public GameObject cube;
	int Active;
	Vector2 Center;
	GameObject pointer;
	
	public List <Vector2> dots = new List<Vector2>();
	
	
	int LeftPos;
	int RightPos;
	int TopPos;
	int DownPos;
	
	int LeftFromCenter;
	int RightFromCenter;
	int TopFromCenter;
	int DownFromCenter;


	GameObject[] insCubes;
	
	public float CheckKoef1;
	public float CheckKoef2;
	
	public GameObject round;
	
	
	public GameObject[] ColliderCube = new GameObject[4];
	
	// Use this for initialization
	void Start () {
		pointer = GameObject.FindGameObjectWithTag ("trail-child");
		cube = GameObject.FindGameObjectWithTag ("cube");
		round = GameObject.FindGameObjectWithTag ("sphere");
		StartInvoke ();
	}

	public void StartInvoke() {

		InvokeRepeating ("Rep", 0.04f, 0.04f);

	}
	
	
	void Rep () {
		
		if (Input.GetMouseButton (0)) {

			Active = 1;
			pointer.GetComponent<MeshRenderer>().enabled = true;
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				dots.Add (hit.point);
			
				Instantiate (cube, new Vector3 (hit.point.x, hit.point.y, 0), Quaternion.identity);
			}
		} else {

			pointer.GetComponent<MeshRenderer>().enabled = false;

			if (Active == 1)
			{
				Active = 2;
			}
		}
		
		
		if (Active == 2) {
			
			DetectCube();
			CancelInvoke("Rep");
			
		}
		
		
		
		
		
	}
	
	
	
	//************************************ДЕТЕКТ РОМБ***********************************************
	
	void DetectCube() {
		
		//------------------------------------------------------------ШУКАЄМО КРАЙНІ----------------------------------
		float MinX = 100;
		float MaxX = -100;
		float MinY = 100;
		float MaxY = -100;
		
		//START
		for (int k = 0; k < dots.Count; k++) {
			
			
			if (dots [k].x < MinX) {
				MinX = dots [k].x;
				LeftPos = k;
			}
			
			if (dots [k].x > MaxX) {
				MaxX = dots [k].x;
				RightPos = k;
			}
			
			
			if (dots [k].y > MaxY) {
				MaxY = dots [k].y;
				TopPos = k;
			}
			
			if (dots [k].y < MinY) {
				MinY = dots [k].y;
				DownPos = k;
			}
			
			
		}
	

		
		GetComponent<CountRomb>().GetDataCube(dots [LeftPos], dots [TopPos], dots [RightPos], dots [DownPos]);
	}


	
}
