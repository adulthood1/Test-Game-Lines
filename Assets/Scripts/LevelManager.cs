using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

public enum Figure {
		triangle,
		romb,
		cube,
		other

	}

	public Figure Shape;
	public Text ScoreFinal;
	public GameObject table;

	public int Score = 0;
	public int Level = 0;

	private bool defaultSwitch;

	bool Triangle;

	public Vector2[] TriangleCoordinates = new Vector2[3];
	public Vector2[] CubeCoordinates = new Vector2[4];
	public Vector2[] Coordinates;


	public GameObject TRIANGLE;
	public GameObject CUBE;
	public GameObject ROMB;
	public GameObject NEW_MANAGER;

	float time;
	int aTime;
	public Text forTime;
	public Text forScore;

	void Start () {


		defaultSwitch = false;

		if (Controller.data.Level > 0) {
			Level = Controller.data.Level;
		} else {
			Level = 0;
			Controller.data.Level = 0;
		}



		if (Controller.data.Score > 0) {
			Score = Controller.data.Score;
		} else {
			Score = 0;
			Controller.data.Score = 0;
		}


		
		forScore.text = "Счет: " + Score.ToString ();

		if (Controller.data.Lost)
		{	time = Controller.data.time;
		} else {
			time = 20 - Level/5; //НАСКІЛЬКИ ЗМЕНШУЄТЬСЯ ЧАС
		}

	
		switch (Level) {//який модуль використовувати для контроля, яка фігура, які координати
		
		case 1:
			Shape = Figure.other;
			Coordinates = new Vector2[7];
			Coordinates [0] = new Vector2 (1, 1);
			Coordinates [1] = new Vector2 (2, 4);
			Coordinates [2] = new Vector2 (5, 6);
			Coordinates [3] = new Vector2 (7, 0);
			Coordinates [4] = new Vector2 (4, -1);
			Coordinates [5] = new Vector2 (2, -4);
			Coordinates [6] = new Vector2 (-5f, -8);
			break;


		case 2:
			Shape = Figure.other;
			Coordinates = new Vector2[6];
			Coordinates [0] = new Vector2 (-4, 5);
			Coordinates [1] = new Vector2 (-3, 8);
			Coordinates [2] = new Vector2 (2, 10);
			Coordinates [3] = new Vector2 (3, 7);
			Coordinates [4] = new Vector2 (5, 3);
			Coordinates [5] = new Vector2 (0, -4);
			break;

		case 3:
			Shape = Figure.other;
			Coordinates = new Vector2[5];
			Coordinates [0] = new Vector2 (-4, 6);
			Coordinates [1] = new Vector2 (-2, 9);
			Coordinates [2] = new Vector2 (4, 10);
			Coordinates [3] = new Vector2 (8, 7);
			Coordinates [4] = new Vector2 (5, -2);
			break;
		
		case 21:
			Shape = Figure.triangle;
			TriangleCoordinates [0] = new Vector2 (2, 5);
			TriangleCoordinates [1] = new Vector2 (7, -1);
			TriangleCoordinates [2] = new Vector2 (8, 9);

			break;

		case 25:
			Shape = Figure.cube;

			CubeCoordinates [0] = new Vector2 (-7, 0); //ліво низ
			CubeCoordinates [1] = new Vector2 (-7, 7); //ліво верх
			CubeCoordinates [2] = new Vector2 (5, 7); //право верх
			CubeCoordinates [3] = new Vector2 (5, 0); //право низ

			break;

		case 36:
			Shape = Figure.romb;
			
			CubeCoordinates [0] = new Vector2 (-7, 4); //ліво 
			CubeCoordinates [1] = new Vector2 (0, 8); //верх
			CubeCoordinates [2] = new Vector2 (5, 3); //право
			CubeCoordinates [3] = new Vector2 (1, 0); // низ
			break;

		case 48:
			Shape = Figure.triangle;
			TriangleCoordinates [0] = new Vector2 (2, 5);
			TriangleCoordinates [1] = new Vector2 (7, -1);
			TriangleCoordinates [2] = new Vector2 (8, 9);
			break;



		default:
			defaultSwitch = true;





			if (Controller.data.Lost == false)
			{


				int R = Random.Range (1,3);



			if (R == 1) {

				Shape = Figure.triangle;
				Controller.data.triangle = true;
				Controller.data.cube = false;
				Controller.data.romb = false;
				TriangleCoordinates [0] = new Vector2 (Random.Range (-3f, 3f), Random.Range (-3f, 3f));
				TriangleCoordinates [1] = new Vector2 (Random.Range (8f, 12f), Random.Range (-4f, 5f));
				TriangleCoordinates [2] = new Vector2 (Random.Range (4f, 5f), Random.Range (9f, 12f));
					 
			} else if (R == 2) {

				Shape = Figure.romb;
					Controller.data.triangle = false;
					Controller.data.cube = false;
					Controller.data.romb = true;
				CubeCoordinates [0] = new Vector2 (Random.Range (-3f, 3f), Random.Range (-3f, 3f));
				CubeCoordinates [1] = new Vector2 (Random.Range (4f, 5f), Random.Range (9f, 12f));
				CubeCoordinates [2] = new Vector2 (Random.Range (8f, 12f), Random.Range (-4f, 5f));
				CubeCoordinates [3] = new Vector2 (Random.Range (4f, 5f), Random.Range (-9f, -12f));
			} else if (R == 3) {

				Shape = Figure.cube;
					Controller.data.cube = true;
					Controller.data.triangle = false;
					Controller.data.romb = false;
				float var1 = Random.Range (-3f, 3f);
				float var2 = Random.Range (-3f, 3f);
				float var3 = Random.Range (10f, 15f);
				float var4 = Random.Range (7f, 10f);

				CubeCoordinates [0] = new Vector2 (var1, var2);//ліва низ   
				CubeCoordinates [1] = new Vector2 (var1, var2 + var3);//ліва верх 
				CubeCoordinates [2] = new Vector2 (var4, var3 + var2); //права верх
				CubeCoordinates [3] = new Vector2 (var4, var2); //права низ

			}

		} else if (Controller.data.Lost)
		{
			if (Controller.data.cube || Controller.data.romb)
				{
					Shape = Figure.cube;
					CubeCoordinates = Controller.data.cubeVertices;

				} else if (Controller.data.triangle)
				{
					Shape = Figure.triangle;
					TriangleCoordinates = Controller.data.triangleVertices;

				}
		}

			break;
		}


		if (Shape == Figure.other) {
			AwakeOTHERCheck (Coordinates);
		} else {
			AwakeCheck();
		}
	
	}

	void AwakeOTHERCheck(Vector2[] otherPoses){

			NEW_MANAGER.SetActive(true);

			NEW_MANAGER.GetComponent<CountNEW_MANAGER>().Create(otherPoses);
			
		}

	void Update(){


		time -= Time.deltaTime;

		if (time < 0) {
			LoseOnTime();
		}

		aTime = (int)time;
		forTime.text = "Время: " + aTime.ToString();

	}


	void AwakeCheck(){


		if (Shape == Figure.triangle) {
			TRIANGLE.SetActive (true);
			TRIANGLE.GetComponent<CountTriangle> ().Create (TriangleCoordinates [0], 
			                                                TriangleCoordinates [1], 
			                                                TriangleCoordinates [2]); 
			Controller.data.triangleVertices = TriangleCoordinates;


		} else if (Shape == Figure.cube) {
			CUBE.SetActive(true);
			CUBE.GetComponent<CountCube>().Create(CubeCoordinates[0],
			                                      CubeCoordinates[1], 
			                                      CubeCoordinates[2], 
			                                      CubeCoordinates[3]);
			Controller.data.cubeVertices = CubeCoordinates;

		} else if (Shape == Figure.romb) {
			ROMB.SetActive(true);
			ROMB.GetComponent<CountRomb>().Create(CubeCoordinates[0],
			                                      CubeCoordinates[1], 
			                                      CubeCoordinates[2], 
			                                      CubeCoordinates[3]);
			Controller.data.cubeVertices = CubeCoordinates;
			
		} 
	}


	public void Win(){
		Controller.data.Lost = false;
		forScore.text = "Счет: " + Score.ToString ();
		Controller.data.Score ++;
		Controller.data.Level ++;

		Controller.data.triangleVertices = null;
		Controller.data.cubeVertices = null;
	



		Application.LoadLevel (Application.loadedLevelName);
	}


	public void Lose(){
		Controller.data.time = time;
		Controller.data.Lost = true;



		if (defaultSwitch) {
			if (Shape == Figure.triangle) {



				Controller.data.triangleVertices = new Vector2[3];
				Controller.data.triangleVertices = TriangleCoordinates;
			} else if (Shape == Figure.romb || Shape == Figure.cube) {



				Controller.data.cubeVertices = new Vector2[4];
				Controller.data.cubeVertices = CubeCoordinates;
			}
		}

		Application.LoadLevel (Application.loadedLevelName);
	}


	public void LoseOnTime(){
		ScoreFinal.text = "Счет: " + Score.ToString ();
		table.SetActive (true);
	}

	public void Restart() {
		Controller.data.Score = 0;
		Controller.data.Level = 0;
		Controller.data.Lost = false;
		Controller.data.triangleVertices = null;
		Controller.data.cubeVertices = null;
		Application.LoadLevel (Application.loadedLevelName);


	}
	

}
