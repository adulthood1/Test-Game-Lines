using UnityEngine;
using System.Collections;

public class Trail : MonoBehaviour {

	Vector2 pos;
	Ray ray;
	RaycastHit hit;


	void Start(){
		pos = gameObject.transform.position;

	}

	void Update () {
	if (Input.GetMouseButton (0)) {

			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				pos = hit.point;
				gameObject.transform.position = pos;
			}

		}
	}
}
