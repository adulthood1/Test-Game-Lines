using UnityEngine;
using System.Collections;

public class CountTriangle : MonoBehaviour {
	

	public Vector2 VarY;
	public Vector2 Left;
	public Vector2 Right;

	float ExH_OUR;
	float ExH;

	public float BaseSide;
	public float LeftSide;
	public float RightSide;

	public float ExampleBaseLeft;
	public float ExampleBaseRight;

	float ExampleHeightDifference;
	float SideHeightDiff;
	int OkFirst = 0;
	float KoeffMistake;


	public GameObject Vertices;

	Vector2[] Position = new Vector2[3];

	float BaseLeft;
	float BaseRight;

	public GameObject[] TriangleVertices = new GameObject[3];

	public void Create (Vector2 Vert0, Vector2 Vert1, Vector2 Vert2)
	{

		Position [0] = Vert0;
		Position [1] = Vert1;
		Position [2] = Vert2;
	

	for (int K = 0; K < 3; K++)
	{

		Instantiate (Vertices, Position[K], Quaternion.identity);
		

	}
		TriangleVertices = GameObject.FindGameObjectsWithTag("example");

	}


	void Start () {


		float minX = 100;
		float maxX = -100;
		
		
		int Lefty = 0;
		int Righty = 0;
		int Base = 0;
		
		for (int k = 0; k < 3; k ++) {


			
			if (TriangleVertices [k].transform.position.x < minX) {
				minX = TriangleVertices [k].transform.position.x;
				Lefty = k;
			}
			
			if (TriangleVertices [k].transform.position.x > maxX) {
				maxX = TriangleVertices [k].transform.position.x;
				Righty = k;
			}
			
			
			
		}
		
		if (Lefty == 0 && Righty == 1) {
			Base = 2;
		} else if (Lefty == 0 && Righty == 2) {
			Base = 1;
		} else if (Lefty == 1 && Righty == 2) {
			Base = 0;
		} else if (Lefty == 1 && Righty == 0) {
			Base = 2;
		} else if (Lefty == 2 && Righty == 0) {
			Base = 1;
		} else if (Lefty == 2 && Righty == 1) {
			Base = 0;
		}


		for (int i = 0; i < 3; i ++)

		{

			if (i != 2) {
				LineRenderer lineRenderer = TriangleVertices[i].GetComponent<LineRenderer> ();
				lineRenderer.SetPosition (0, TriangleVertices[i].transform.position);
				lineRenderer.SetPosition (1, TriangleVertices[i + 1].transform.position);
				
				//lineRenderer.SetPosition (k, allspheres[k + 1].transform.position);
				
			} else {
				
				LineRenderer lineRenderer = TriangleVertices[2].GetComponent<LineRenderer> ();
				lineRenderer.SetPosition (0, TriangleVertices[2].transform.position);
				lineRenderer.SetPosition (1, TriangleVertices[0].transform.position);
			}


		}





		//РАХУЄМО ТРИКУТНИК - ПРИКЛАД
		float ExampleBaseSide = Vector2.Distance (TriangleVertices[Lefty].transform.position, TriangleVertices[Righty].transform.position);  //основа чи верхівка
		float ExampleeLeftSide = Vector2.Distance (TriangleVertices[Lefty].transform.position, TriangleVertices[Base].transform.position); //ліва
		float ExampleRightSide = Vector2.Distance (TriangleVertices[Righty].transform.position, TriangleVertices[Base].transform.position); //права
		
		 ExampleBaseLeft = ExampleBaseSide / ExampleeLeftSide;
		 ExampleBaseRight = ExampleBaseSide / ExampleRightSide;


		//РАХУЄМО ВИСОТУ - РІЗНИЦЮ МІЖ БОКОВИМИ В ПРИКЛАДІ
		
		
		if (TriangleVertices [Lefty].transform.position.y < TriangleVertices [Righty].transform.position.y) {
			ExampleHeightDifference = TriangleVertices [Righty].transform.position.y - TriangleVertices [Lefty].transform.position.y;
		} else if (TriangleVertices [Lefty].transform.position.y > TriangleVertices [Righty].transform.position.y) {
			ExampleHeightDifference = TriangleVertices [Lefty].transform.position.y - TriangleVertices [Righty].transform.position.y;
		} else if (TriangleVertices [Lefty].transform.position.y == TriangleVertices [Righty].transform.position.y){
			ExampleHeightDifference = 0;
		}

		ExH = ExampleBaseLeft / ExampleHeightDifference;

		
		/*Top.GetComponent<LineRenderer> ().SetPosition (0, Right.transform.position);
		Top.GetComponent<LineRenderer> ().SetPosition (1, Down.transform.position);
		
		Right.GetComponent<LineRenderer> ().SetPosition (0, Right.transform.position);
		Right.GetComponent<LineRenderer> ().SetPosition (1, Top.transform.position);
		
		Left.GetComponent<LineRenderer> ().SetPosition (0, Left.transform.position);
		Left.GetComponent<LineRenderer> ().SetPosition (1, Top.transform.position);*/
		
		/*	Down.GetComponent<LineRenderer> ().SetPosition (0, Left.transform.position);
		Down.GetComponent<LineRenderer> ().SetPosition (1, Down.transform.position);

		
	}*/
	}


	public void GetDataTriangle(Vector2 One, Vector2 Two, Vector2 Three) {

		VarY = One;
		Left = Two;
		Right = Three;

		float BaseSide = Vector2.Distance (Left, Right);  //основа чи верхівка
		float LeftSide = Vector2.Distance (Left, VarY); //ліва
		float RightSide = Vector2.Distance (Right, VarY); //права


		 BaseLeft = BaseSide / LeftSide;
		 BaseRight = BaseSide / RightSide;


		Compare ();

	}


		void Compare(){

		//РАХУЄМО СТОРОНИ ОСНОВНОЇ ФІГУРИ, ПОРІВНЮЄМО

		float MistakePercent = 3; //на скільки ділиться величина, похибка

		

		if ((BaseLeft > (ExampleBaseLeft - ExampleBaseLeft / MistakePercent) && 
			BaseLeft < (ExampleBaseLeft + ExampleBaseLeft / MistakePercent)) 
		    &&
			BaseRight > (ExampleBaseRight - ExampleBaseRight / MistakePercent) && 
			BaseRight < (ExampleBaseRight + ExampleBaseRight / MistakePercent)) {

			OkFirst = 1;

		} else {

			GameObject.FindGameObjectWithTag("levelM").GetComponent<LevelManager>().Lose();

		}


		//РАХУЄМО ВИСОТИ БОКОВИХ ОСНОВНОЇ ФІГУРИ, ПОРІВНЮЄМО

		if (Left.y < Right.y) {
			 SideHeightDiff = Right.y - Left.y;
		} else if (Left.y > Right.y) {
			 SideHeightDiff = Left.y - Right.y;
		} else {
			 SideHeightDiff = 0;
		}


		ExH_OUR = BaseLeft / SideHeightDiff;



		KoeffMistake = 3;



		//ПОРІВНЮЄМО РІЗНИЦЮ В ВИСОТІ МІЖ БІЧНИМИ ТОЧКАМИ


		if (ExampleHeightDifference != 0) {
			if (ExH_OUR > ExH - ExH / KoeffMistake && 
				ExH_OUR < ExH + ExH / KoeffMistake) {
				if (OkFirst == 1)
				{
					GameObject.FindGameObjectWithTag("levelM").GetComponent<LevelManager>().Win();
				}
			} else {
				GameObject.FindGameObjectWithTag("levelM").GetComponent<LevelManager>().Lose();			}
		} else {
			if (SideHeightDiff > 0 && SideHeightDiff < 1)
			{
				GameObject.FindGameObjectWithTag("levelM").GetComponent<LevelManager>().Win();
			} else {
				GameObject.FindGameObjectWithTag("levelM").GetComponent<LevelManager>().Lose();

			}
		}
	 


	}

}
