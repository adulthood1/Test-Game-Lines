using UnityEngine;
using System.Collections;

public class RestartButton : MonoBehaviour {

	public void Res () {
		Application.LoadLevel (Application.loadedLevelName);
	}
}
