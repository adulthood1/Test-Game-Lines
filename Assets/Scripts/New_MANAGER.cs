using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class New_MANAGER : MonoBehaviour {
	
	Ray ray;
	RaycastHit hit;
	GameObject cube;
	int Active;
	Vector2 Center;
	int Correcttimes = 0;

	public List <int> NoBorders = new List<int>();
	
	public List <float> ExampleFROM_TOP = new List<float>();
	public List <float> ExampleFROM_DOWN = new List<float>();
	public List <float> ExampleFROM_RIGHT = new List<float>();
	public List <float> ExampleFROM_LEFT = new List<float>();


	public List <float> dist_to_Top = new List<float>();
	public List <float> dist_to_Left = new List<float>();
	public List <float> dist_to_Right = new List<float>();
	public List <float> dist_to_Down = new List<float>();
	
	int NoBordersToSearch = 0;

	public List <int> Closestpoint = new List<int>();

	public List <Vector2> dots = new List<Vector2>();
	
	GameObject pointer;

	int LeftPos;
	int RightPos;
	int TopPos;
	int DownPos;
	
	int LeftFromCenter;
	int RightFromCenter;
	int TopFromCenter;
	int DownFromCenter;

	public GameObject round;
	
	
	public GameObject[] ColliderCube = new GameObject[4];
	
	// Use this for initialization
	void Start () {
		pointer = GameObject.FindGameObjectWithTag ("trail-child");
		cube = GameObject.FindGameObjectWithTag ("cube");
		round = GameObject.FindGameObjectWithTag ("sphere");
		InvokeRepeating ("Rep", 0.04f, 0.04f);
	}

	//ДОДАЄМО СПІВВІДНОШЕННЯ

	public void toTOP_LIST(float ToTop) {
		ExampleFROM_TOP.Add (ToTop);
	}
	public void toDOWN_LIST(float ToDown) {
		ExampleFROM_DOWN.Add (ToDown);
	}
	public void toLEFT_LIST(float ToLeft) {
		ExampleFROM_LEFT.Add (ToLeft);
	}
	public void toRIGHT_LIST(float ToRight) {
		ExampleFROM_RIGHT.Add (ToRight);
	}

	public void NoBordersINT (int var){
		NoBordersToSearch = var;
	}




	void Rep () {
		
		if (Input.GetMouseButton (0)) {
			
			GameObject.FindGameObjectWithTag ("trail").GetComponent<TrailRenderer> ().time = 4;
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


		float topDown = Vector2.Distance (dots [TopPos], dots [DownPos]);

		for (int Z = 0; Z < NoBordersToSearch; Z++)
		{

		float diff = 100;
		float D1 = 0;
		float D2 = 0;
		float D3 = 0;
		float D4 = 0;
			int ClosePoint = 0;


		
		for (int k = 0; k < dots.Count; k++) {

			float distTOP = Vector2.Distance (dots [k], dots [TopPos]);
			float distLEFT = Vector2.Distance (dots [k], dots [LeftPos]);
			float distRIGHT = Vector2.Distance (dots [k], dots [RightPos]);
			float distDOWN = Vector2.Distance (dots [k], dots [DownPos]);// ще такі ж для решти
				
			float Dii = topDown / distTOP;
			float Dii2 = topDown / distDOWN;
			float Dii3 = topDown / distRIGHT;
			float Dii4 = topDown / distLEFT;



			if (ExampleFROM_TOP[Z] > Dii) {
				D1 = ExampleFROM_TOP[Z] - Dii;
			} else {
					D1 = Dii - ExampleFROM_TOP[Z];
			}


				if (ExampleFROM_DOWN[Z] > Dii2) {
				D2 = ExampleFROM_DOWN[Z] - Dii2;
			} else {
					D2 = Dii2 - ExampleFROM_DOWN[Z];
			}

				if (ExampleFROM_RIGHT[Z] > Dii3) {
					D3 = ExampleFROM_RIGHT[Z] - Dii3;
			} else {
					D3 = Dii3 - ExampleFROM_RIGHT[Z];
			}

				if (ExampleFROM_LEFT[Z] > Dii4) {
					D4 = ExampleFROM_LEFT[Z] - Dii4;
			} else {
					D4 = Dii4 - ExampleFROM_LEFT[Z];
			}
			
			
			float Dsum = D1 + D2 + D3 + D4;


			if (Dsum < diff) {
				diff = Dsum;
				ClosePoint = k;

			}
		}
			Closestpoint.Add(ClosePoint);

		
	}
		Check (Closestpoint.Count);
	}


	void Check(int var){

		for (int D = 0; D < var; D++) {

			int CloseP = Closestpoint [D];


			float distTOP_to_DOWN_Check = Vector2.Distance (dots [TopPos], dots [DownPos]);
			float distTO_Top_Check = Vector2.Distance (dots [CloseP], dots [TopPos]);
			float distTO_Left_Check = Vector2.Distance (dots [CloseP], dots [LeftPos]);
			float distTO_Right_Check = Vector2.Distance (dots [CloseP], dots [RightPos]);
			float distTO_Down_Check = Vector2.Distance (dots [CloseP], dots [DownPos]);

			float tmp1 = distTOP_to_DOWN_Check / distTO_Top_Check;
			float tmp2 = distTOP_to_DOWN_Check / distTO_Left_Check;
			float tmp3 = distTOP_to_DOWN_Check / distTO_Right_Check;
			float tmp4 = distTOP_to_DOWN_Check / distTO_Down_Check;


			float MistakePercent = 5;
		

			if (tmp1 > (ExampleFROM_TOP [D] - ExampleFROM_TOP [D] / MistakePercent) && 
				tmp1 < (ExampleFROM_TOP [D] + ExampleFROM_TOP [D] / MistakePercent) 
			    &&
				tmp2 > (ExampleFROM_LEFT [D] - ExampleFROM_LEFT [D] / MistakePercent) && 
				tmp2 < (ExampleFROM_LEFT [D] + ExampleFROM_LEFT [D] / MistakePercent) 
			    &&
				tmp3 > (ExampleFROM_RIGHT [D] - ExampleFROM_RIGHT [D] / MistakePercent) && 
				tmp3 < (ExampleFROM_RIGHT [D] + ExampleFROM_RIGHT [D] / MistakePercent) 
			    &&
				tmp4 > (ExampleFROM_DOWN [D] - ExampleFROM_DOWN [D] / MistakePercent) && 
				tmp4 < (ExampleFROM_DOWN [D] + ExampleFROM_DOWN [D] / MistakePercent) 
			    ) {
				Correcttimes++;
			}  
		}


		if (Correcttimes == var)
		{
			GameObject.FindGameObjectWithTag("levelM").GetComponent<LevelManager>().Win ();
		} else {
			GameObject.FindGameObjectWithTag("levelM").GetComponent<LevelManager>().Lose();
		}
		
	}

	
	
}
