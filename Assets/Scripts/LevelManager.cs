using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Singleton
    public static LevelManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            myAwake();
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }
    #endregion

    public GameObject[] levels;
    GameObject currentLevel;
    GameObject nextLevel;
    private void myAwake()
    {
        currentLevel = Instantiate(levels[Random.Range(0, levels.Length)]);
        nextLevel = Instantiate(levels[Random.Range(0, levels.Length)],new Vector3(0,0,220),Quaternion.identity);
    }

    public void SpawnLevel()
    {
        print("spawning ahead");
        Destroy(currentLevel);
        currentLevel = nextLevel;
        nextLevel = Instantiate(levels[Random.Range(0, levels.Length)],currentLevel.transform.position+new Vector3(0,0,220),Quaternion.identity);
    }

}
