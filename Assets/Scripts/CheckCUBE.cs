using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckCUBE : MonoBehaviour {

	Ray ray;
	RaycastHit hit;
	public GameObject cube;
	int Active;
	Vector2 Center;
	GameObject pointer;

	public List <Vector2> dots = new List<Vector2>();

	bool ifCUBE = false;

	int LeftPos;
	int RightPos;
	int TopPos;
	int DownPos;

	int LeftFromCenter;
	int RightFromCenter;
	int TopFromCenter;
	int DownFromCenter;



	public float CheckKoef1;
	public float CheckKoef2;

	public GameObject round;


	public GameObject[] ColliderCube = new GameObject[4];

	// Use this for initialization
	void Start () {
		pointer = GameObject.FindGameObjectWithTag ("trail-child");
		cube = GameObject.FindGameObjectWithTag ("cube");
		round = GameObject.FindGameObjectWithTag ("sphere");
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

	

//************************************ДЕТЕКТ КУБ***********************************************

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

	/*	Instantiate (round, dots [TopPos], Quaternion.identity);
		Instantiate (round, dots [LeftPos], Quaternion.identity);
		Instantiate (round, dots [RightPos], Quaternion.identity);
		Instantiate (round, dots [DownPos], Quaternion.identity);

		Debug.Log ("TopPos: " + TopPos);
		Debug.Log ("LeftPos: " + LeftPos);
		Debug.Log ("RightPos: " + RightPos);
		Debug.Log ("DownPos: " + DownPos);*/


		CenterSearch ();
	}

		//------------------------------------------------------------ШУКАЄМО ЦЕНТР----------------------------------
		
		void CenterSearch(){


		Vector2 centerVerical = Vector2.Lerp (dots [TopPos], dots [DownPos], 0.5f);
		Vector2 centerHorizontal = Vector2.Lerp (dots [LeftPos], dots [RightPos], 0.5f);

		Center = new Vector2 (centerHorizontal.x, centerVerical.y);

		//AdditionalPoints ();

		CheckIfCube ();
	}


		void CheckIfCube() {

		//ПЕРЕВІРЯЄМО ВІДСТАНІ ВІД ЛІВОЇ ДО ВЕРНХЬОЇ 

		if (TopPos > LeftPos) {

			int timesOK = 0;

			for (int K = LeftPos; K < TopPos; K++) {
				float dist = Vector2.Distance (dots [K], Center);
				float distTOC = Vector2.Distance (dots [TopPos], Center);

				if (dist > distTOC) {
					timesOK ++;
				}

			}

			if ((TopPos - LeftPos)/2f < timesOK) {
				ifCUBE = true;
			}

		} else {

			if (TopPos < LeftPos) {
				
				int timesOK = 0;
				
				for (int K = TopPos; K < LeftPos; K++)
				{
					float dist = Vector2.Distance (dots[K], Center);
					float distTOC = Vector2.Distance (dots[TopPos], Center);

					if (dist > distTOC)
					{
						timesOK ++;

					}
					
				}

				if ((TopPos - LeftPos)/2f < timesOK)
				{
					ifCUBE = true;
				}
				
			}


		}




		CenterSides ();
	}


		void CenterSides() {


		Vector2 LeftSideCenter = new Vector2 (dots [LeftPos].x, Center.y);
		Vector2 RightSideCenter = new Vector2 (dots [RightPos].x, Center.y);

		Vector2 TopSideCenter = new Vector2 (Center.x, dots [TopPos].y);
		Vector2 DownSideCenter = new Vector2 (Center.x, dots [DownPos].y);



		int CloseToTOP = 0;
		int CloseToDown = 0;
		int CloseToLeft = 0;
		int CloseToRight = 0;

		float DistToTop = 100;
		float DistToLeft = 100;
		float DistToRight = 100;
		float DistToDown = 100;

		for (int k = 0; k < dots.Count; k++) {

			if (Vector2.Distance (dots[k], LeftSideCenter) < DistToLeft)
			{
				DistToLeft = Vector2.Distance (dots[k], LeftSideCenter);
				CloseToLeft = k;
			}

			if (Vector2.Distance (dots[k], RightSideCenter) < DistToRight)
			{
				DistToRight = Vector2.Distance (dots[k], RightSideCenter);
				CloseToRight = k;
			}

			if (Vector2.Distance (dots[k], TopSideCenter) < DistToTop)
			{
				DistToTop = Vector2.Distance (dots[k], TopSideCenter);
				CloseToTOP = k;
			}

			if (Vector2.Distance (dots[k], DownSideCenter) < DistToDown)
			{
				DistToDown = Vector2.Distance (dots[k], DownSideCenter);
				CloseToDown = k;
			}



		}


		if (ifCUBE) {
			GetComponent<CountCube> ().GetDataCube (dots [CloseToLeft], dots [CloseToTOP], dots [CloseToRight], dots [CloseToDown]);
		} else {
			GameObject.FindGameObjectWithTag("levelM").GetComponent<LevelManager>().Lose();
		}
		
		}





}
