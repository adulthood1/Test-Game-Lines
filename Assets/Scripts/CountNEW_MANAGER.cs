using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CountNEW_MANAGER : MonoBehaviour {
	

	public Vector2 Top;
	public Vector2 Down;
	public Vector2 Left;
	public Vector2 Right;


	public GameObject sphere;
	public GameObject[] allspheres;

	
	public List <int> NoBorders = new List<int>();
	
	public List <float> TD_toTOP = new List<float>();
	public List <float> TD_toDOWN = new List<float>();
	public List <float> TD_toRIGHT = new List<float>();
	public List <float> TD_toLEFT = new List<float>();
	

	public List <Vector2> NoBorderCoords = new List<Vector2>();

	float ExampleHeightDownDiff;
	float HeightDif;
	 
	float ExampleKoef; //ПЕРЕВІРЯЄ ОСІ
	float ExampleKoef2; //ПЕРЕВІРЯЄ ДВІ БОКОВІ СТОРОНИ МІЖ ЦЕНТРАЛЬНИМИ ЛІНІЯМИ
	float OurKoef;
	float OurKoef2;

	public int LeftPos = 0;
	public int RightPos = 0;
	public int TopPos = 0;
	public int DownPos = 0;


	float TopDown;
	float LeftRight;
	Vector2[] allposes;

	public GameObject Vertices;
	Vector2[] Position;

	public GameObject[] CubeVertices = new GameObject[4];
	

	public void Create (Vector2[] allposes)
	{

		int M = allposes.Length;
		Position = new Vector2[M];
		Position = allposes;


	for (int K = 0; K < M; K++)
	{

		Instantiate (Vertices, Position[K], Quaternion.identity);
		

	}
		allspheres = GameObject.FindGameObjectsWithTag("example");

	}


	void Start () {


		float MinX = 100;
		float MaxX = -100;
		float MinY = 100;
		float MaxY = -100;
		
		
		
		//ПЕРЕБИРАЄМО ВСІ ПО х, у, ШУКАЄМО КРАЙНІ;
		for (int k = 0; k < allspheres.Length; k++) {
			
			if (k != allspheres.Length - 1) {
				LineRenderer lineRenderer = allspheres [k].GetComponent<LineRenderer> ();
				lineRenderer.SetPosition (0, allspheres [k].transform.position);
				lineRenderer.SetPosition (1, allspheres [k + 1].transform.position);
				
				//lineRenderer.SetPosition (k, allspheres[k + 1].transform.position);
				
			} else {
				
				LineRenderer lineRenderer = allspheres [k].GetComponent<LineRenderer> ();
				lineRenderer.SetPosition (0, allspheres [k].transform.position);
				lineRenderer.SetPosition (1, allspheres [0].transform.position);
			}
			
			
			if (allspheres [k].transform.position.x < MinX) {
				MinX = allspheres [k].transform.position.x;
				LeftPos = k;

			}
			
			if (allspheres [k].transform.position.x > MaxX) {
				MaxX = allspheres [k].transform.position.x;
				RightPos = k;

			}
			
			
			if (allspheres [k].transform.position.y > MaxY) {
				MaxY = allspheres [k].transform.position.y;
				TopPos = k;

			}
			
			if (allspheres [k].transform.position.y < MinY) {
				MinY = allspheres [k].transform.position.y;
				DownPos = k;

			}
			
		}

		//ПЕРЕБИРАЄМО ЩЕ РАЗ, ШУКАЄМО НЕ КРАЙНІ;
		for (int k = 0; k < allspheres.Length; k++) {
			
			if (k != TopPos && k != LeftPos && k != RightPos && k != DownPos)
			{
				NoBorders.Add(k);
				NoBorderCoords.Add(allspheres[k].transform.position);
			}
			
		}
		
		
		for (int l = 0; l < NoBorders.Count; l++) {
			
			int Indexer = NoBorders[l];
			
			float distToTOP = Vector2.Distance (allspheres[Indexer].transform.position, allspheres[TopPos].transform.position);
			float distToLEFT = Vector2.Distance (allspheres[Indexer].transform.position, allspheres[LeftPos].transform.position);
			float distToRIGHT = Vector2.Distance (allspheres[Indexer].transform.position, allspheres[RightPos].transform.position);
			float distToDOWN = Vector2.Distance (allspheres[Indexer].transform.position, allspheres[DownPos].transform.position);

			
			float ToptoDown = Vector2.Distance (allspheres[TopPos].transform.position, allspheres[DownPos].transform.position);

			
			float var1 = ToptoDown/distToTOP;
			float var2 = ToptoDown/distToDOWN;
			float var3 = ToptoDown/distToLEFT;
			float var4 = ToptoDown/distToRIGHT;

			gameObject.GetComponent<New_MANAGER>().toTOP_LIST(var1);
			gameObject.GetComponent<New_MANAGER>().toDOWN_LIST(var2);
			gameObject.GetComponent<New_MANAGER>().toLEFT_LIST(var3);
			gameObject.GetComponent<New_MANAGER>().toRIGHT_LIST(var4);
			gameObject.GetComponent<New_MANAGER>().NoBordersINT(NoBorders.Count);



		}

	}


	
}
