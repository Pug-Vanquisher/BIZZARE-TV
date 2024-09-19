using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLevel : MonoBehaviour
{
    public string level;

    public void OpenScene(){
        if (level == "MainMenu")
        {
            List<GameObject> gameObjectsToDestroy =  new List<GameObject>();
            gameObjectsToDestroy.Add(GameObject.Find("Music"));
            gameObjectsToDestroy.Add(GameObject.Find("Player(Clone)"));
            gameObjectsToDestroy.Add(GameObject.Find("UI"));
            Debug.Log(gameObjectsToDestroy.Count);
            foreach (GameObject go in gameObjectsToDestroy)
            {
                Destroy(go);
            }
        }
        SceneManager.LoadScene(level);
    }
}
