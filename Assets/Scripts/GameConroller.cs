using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameConroller : MonoBehaviour {

	public static GameConroller instance = null;

	void Awake ()
	{
		if (instance != null) {
			Destroy (gameObject);
			Debug.Log (Time.time + ": Duplicated copy of Level Manager was destroyed");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}

	void Update ()
	{

		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);	
		}

	}

	public void BrickDestroyed ()
	{
		if (Brick.bricksLeft <= 0) {
			Invoke ("LoadNextLevel", 2f);
			GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
			foreach (GameObject ball in balls) {
				//Destroy (ball.gameObject);
				Destroy(ball, 0.5f);
			}
		}
	} 
}
