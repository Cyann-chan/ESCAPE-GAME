using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
    public void ChangeSceneInt(int sceneName)
    {
        Debug.Log("bylmfez");
        SceneManager.LoadScene(sceneName);
    }
    public void Exit()
	{
		Debug.Log("Merci d'avoir joué !");
		Application.Quit();
	}
}
