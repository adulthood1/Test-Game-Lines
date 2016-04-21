using UnityEngine;
using System.Collections;

public class CountCube : MonoBehaviour {
	

	public Vector2 Top;
	public Vector2 Down;
	public Vector2 Left;
	public Vector2 Right;

	public float DownSide;
	public float LeftSide;
	public float RightSide;
	public float TopSide;


	float LD_RT;
	float LT_RD;

	public Vector2 ExampleDownSideMid;
	public Vector2 ExampleLeftSideMid;
	public Vector2 ExampleRightSideMid;
	public Vector2 ExampleTopSideMid;

	float ExampleHeightDownDiff;
	float HeightDif;
	 
	float ExampleKoef; //ПЕРЕВІРЯЄ ОСІ
	float ExampleKoef2; //ПЕРЕВІРЯЄ ДВІ БОКОВІ СТОРОНИ МІЖ ЦЕНТРАЛЬНИМИ ЛІНІЯМИ
	float OurKoef;
	float OurKoef2;
	


	float ExampleTopDown;
	float ExampleLeftRight;

	float TopDown;
	float LeftRight;



	public GameObject Vertices;

	Vector2[] Position = new Vector2[4];



	public GameObject[] CubeVertices = new GameObject[4];

	public void Create (Vector2 Vert0, Vector2 Vert1, Vector2 Vert2, Vector2 Vert3)
	{

		Position [0] = Vert0;
		Position [1] = Vert1;
		Position [2] = Vert2;
		Position [3] = Vert3;
	

	for (int K = 0; K < 4; K++)
	{

		Instantiate (Vertices, Position[K], Quaternion.identity);
		

	}
		CubeVertices = GameObject.FindGameObjectsWithTag("example");

	}


	void Start () {



		for (int i = 0; i < 4; i ++)
			
		{
			
			if (i != 3) {
				LineRenderer lineRenderer = CubeVertices[i].GetComponent<LineRenderer> ();
				lineRenderer.SetPosition (0, CubeVertices[i].transform.position);
				lineRenderer.SetPosition (1, CubeVertices[i + 1].transform.position);
				
				//lineRenderer.SetPosition (k, allspheres[k + 1].transform.position);
				
			} else {
				
				LineRenderer lineRenderer = CubeVertices[3].GetComponent<LineRenderer> ();
				lineRenderer.SetPosition (0, CubeVertices[3].transform.position);
				lineRenderer.SetPosition (1, CubeVertices[0].transform.position);
			}
			
			
		}

	

		//ПРИКЛАД: співвідношення ОСЕЙ
		ExampleDownSideMid = Vector2.Lerp (CubeVertices[0].transform.position, CubeVertices[3].transform.position, 0.5f);  
		ExampleTopSideMid = Vector2.Lerp (CubeVertices[1].transform.position, CubeVertices[2].transform.position, 0.5f);  
		ExampleLeftSideMid = Vector2.Lerp (CubeVertices[0].transform.position, CubeVertices[1].transform.position, 0.5f); 
		ExampleRightSideMid = Vector2.Lerp (CubeVertices[2].transform.position, CubeVertices[3].transform.position, 0.5f); 

		float ExampleMidHorizontal = 0;
		float ExampleMidVertical = 0;

		ExampleMidHorizontal = Vector2.Distance (ExampleLeftSideMid, ExampleRightSideMid);
		ExampleMidVertical = Vector2.Distance (ExampleTopSideMid, ExampleDownSideMid);

		ExampleKoef = ExampleMidVertical / ExampleMidHorizontal;






		//ПРИКЛАД: співвідношення Top-Right та Down-Right
		ExampleDownSideMid = Vector2.Lerp (CubeVertices[0].transform.position, CubeVertices[3].transform.position, 0.5f);  
		ExampleTopSideMid = Vector2.Lerp (CubeVertices[1].transform.position, CubeVertices[2].transform.position, 0.5f);  
		ExampleLeftSideMid = Vector2.Lerp (CubeVertices[0].transform.position, CubeVertices[1].transform.position, 0.5f); 
		ExampleRightSideMid = Vector2.Lerp (CubeVertices[2].transform.position, CubeVertices[3].transform.position, 0.5f); 
		
		float ExampleTopRightMID = Vector2.Distance (ExampleTopSideMid, ExampleRightSideMid);
		float ExampleDownRightMID = Vector2.Distance (ExampleDownSideMid, ExampleRightSideMid);
		

		ExampleKoef2 = ExampleTopRightMID / ExampleDownRightMID;


	}

	//РАХУЄМО ВІДСТАНІ МІЖ ТОЧКАМИ ЯКІ ВВЕДЕНІ ГРАВЦЕМ (СЕРЕДИНИ СТОРІН)
		public void GetDataCube(Vector2 LeftY, Vector2 TopY, Vector2 RightY, Vector2 DownY) {

		Left = LeftY;
		Right = RightY;
		Top = TopY;
		Down = DownY;




		float MidHorizontal = 0;
		float MidVertical = 0;

		MidHorizontal = Vector2.Distance (LeftY, RightY);
		MidVertical = Vector2.Distance (TopY, DownY);

		OurKoef = MidVertical / MidHorizontal;


		OurKoef2 = Vector2.Distance (Top, Right) / Vector2.Distance (Down, Right);





		Compare ();

	}

	//РАХУЄМО СТОРОНИ ОСНОВНОЇ ФІГУРИ, ПОРІВНЮЄМО
		void Compare(){

		float MistakePercent = 7; //на скільки ділиться величина, похибка

		if (OurKoef > (ExampleKoef - ExampleKoef / MistakePercent) && 
			OurKoef < (ExampleKoef + ExampleKoef / MistakePercent)
		
		&& 

			OurKoef2 > (ExampleKoef2 - ExampleKoef2 / MistakePercent) && 
			OurKoef2 < (ExampleKoef2 + ExampleKoef2 / MistakePercent))
		{

			GameObject.FindGameObjectWithTag("levelM").GetComponent<LevelManager>().Win();
		} else {

			GameObject.FindGameObjectWithTag("levelM").GetComponent<LevelManager>().Lose();

		}
	

	}


}
