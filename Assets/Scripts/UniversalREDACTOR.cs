using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UniversalREDACTOR : MonoBehaviour {

	Ray ray;
	RaycastHit hit;
	public GameObject sphere;
	private GameObject[] allspheres;
	int NumberOfDots = 0;

	//int K = 0;

	public List <int> NoBorders = new List<int>();

	public List <Vector2> Coordinates = new List<Vector2>();


	public int LeftPos = 0;
	public int RightPos = 0;
	public int TopPos = 0;
	public int DownPos = 0;


	public void GenerateDot(){
		Vector2 posit = new Vector2(Random.Range (-5, 5), Random.Range (-5, 5)); 
		Instantiate (sphere, posit, Quaternion.identity);
		NumberOfDots ++;
		Debug.Log ("New Dot Generated");
	}


	public void BakePos () {
		allspheres = GameObject.FindGameObjectsWithTag ("sphere");


		foreach (var c in allspheres) {
			Coordinates.Add(c.transform.position);
		}


		float MinX = 100;
		float MaxX = -100;
		float MinY = 100;
		float MaxY = -100;
		
		
		
		//ПЕРЕБИРАЄМО ВСІ ПО х, у, ШУКАЄМО КРАЙНІ;
		for (int k = 0; k < allspheres.Length; k++) {

			if (k != allspheres.Length - 1)
			{
				LineRenderer lineRenderer = allspheres[k].GetComponent<LineRenderer>();
				lineRenderer.SetPosition (0, allspheres[k].transform.position);
				lineRenderer.SetPosition (1, allspheres[k + 1].transform.position);

				//lineRenderer.SetPosition (k, allspheres[k + 1].transform.position);

			} else {

				LineRenderer lineRenderer = allspheres[k].GetComponent<LineRenderer>();
				lineRenderer.SetPosition (0, allspheres[k].transform.position);
				lineRenderer.SetPosition (1, allspheres[0].transform.position);
			}

			
			if (allspheres [k].transform.position.x < MinX) {
				MinX = allspheres[k].transform.position.x;
				LeftPos = k;
			}
			
			if (allspheres [k].transform.position.x > MaxX) {
				MaxX = allspheres[k].transform.position.x;
				RightPos = k;
			}
			
			
			if (allspheres [k].transform.position.y > MaxY) {
				MaxY = allspheres[k].transform.position.y;
				TopPos = k;
			}
			
			if (allspheres [k].transform.position.y < MinY) {
				MinY = allspheres[k].transform.position.y;
				DownPos = k;
			}

		}


		//--------------------------------------------------------------------------------------------------------

		//ПЕРЕБИРАЄМО ЩЕ РАЗ, ШУКАЄМО НЕ КРАЙНІ;
		for (int k = 0; k < allspheres.Length; k++) {
			
			if (k != TopPos && k != LeftPos && k != RightPos && k != DownPos)
			{
				NoBorders.Add(k);
			}
		
		}










	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {

			
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				Vector2 pos = hit.transform.position;

				pos.x = hit.point.x;
				pos.y = hit.point.y;

				hit.transform.position = pos;
			}
		}
	}
}
