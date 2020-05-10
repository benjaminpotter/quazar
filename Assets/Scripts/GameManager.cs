using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    #endregion

    public GameObject planetPrefab;
    public StarSystemData starSystem;

    GameObject player;

    public void DrawStarSystem(StarSystemData data)
    {
        player.GetComponent<StarSystemViewHUD>().GenerateStarSystemHUD(data.GetPlanets());
    }

    public void GenerateStarSystem()
    {
        starSystem = WorldGenerator.GenerateStarSystem();
        IOManager.Save(starSystem);
    }

    public void LoadStarSystem()
    {
        // load the star system scene
        SceneManager.LoadScene(1);
    }

    void OnLevelWasLoaded(int level)
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (level == 1)
        {
            DrawStarSystem(IOManager.Load());
        }
    }
}
