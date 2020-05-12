using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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

    public GameObject planetSystemViewPrefab;
    public StarSystemData starSystem;

    public GameObject planetPrefab;
    PlanetData currentPlanet;
    bool canLoadPlanet = true;

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

    public void LoadPlanet(PlanetData planet)
    {
        if (!canLoadPlanet)
            return;

        currentPlanet = planet;
        SceneManager.LoadScene(2);
    }

    void OnLevelWasLoaded(int level)
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (level == 1)
        {
            DrawStarSystem(IOManager.Load());
        }

        if (level == 2)
        {
            GameObject inst = Instantiate(planetPrefab);
            inst.GetComponent<Planet>().GenerateMesh(currentPlanet);
        }
    }
}
