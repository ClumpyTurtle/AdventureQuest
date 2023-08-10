using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckingObjects : MonoBehaviour
{
    public List<string> objectsToDestroy = new List<string> { };
    private static CheckingObjects instance;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        for (var i = 0; i < objectsToDestroy.Count; i++)
        {
            Destroy(GameObject.Find(objectsToDestroy[i]));
        }
    }

    [System.Obsolete]
    void Start()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
            for (var i = 0; i < objectsToDestroy.Count; i++)
            {
                Debug.Log(i);
                //Destroy(GameObject.Find(objName));
            }
        } else {
            Destroy(this.gameObject);
        }
        
    }
}
