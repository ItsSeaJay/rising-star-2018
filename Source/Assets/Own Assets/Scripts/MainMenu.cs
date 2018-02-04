using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad = "Game";
    [SerializeField]
    private string startButton = "Fire1";

	void Start ()
    {
		
	}
	
	void Update ()
    {
        if (Input.GetButtonDown(startButton))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
	}
}
