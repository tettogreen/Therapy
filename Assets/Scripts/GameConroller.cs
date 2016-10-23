using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameConroller : MonoBehaviour {


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
