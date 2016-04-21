using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public static Controller data;

	public Vector2[] triangleVertices;
	public Vector2[] cubeVertices;

	public bool Lost;
	public int Score;
	public int Level;
	public float time;

	public bool cube;
	public bool triangle;
	public bool romb;

	void Awake() {

		if (data == null) {
			DontDestroyOnLoad(gameObject);
			data = this;
		}
		else if (data != this)
		{
			Destroy(gameObject);
		}
	}
}
