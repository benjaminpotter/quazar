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
            Destroy(this.gameObject);
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

    public void SetSystemData(StarSystemData data)
    {
        starSystem = data;
        LoadStarSystem();
    }

    public void LoadStarSystem()
    {
        if (starSystem.GetPlanets() == null)
        {
            starSystem = IOManager.Load();
        }

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

    public void LoadMultiplayer()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadStart()
    {
        SceneManager.LoadScene(0);
    }

    void OnLevelWasLoaded(int level)
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (level == 1)
        {
            DrawStarSystem(starSystem);
        }

        if (level == 2)
        {
            GameObject inst = Instantiate(planetPrefab);
            inst.GetComponent<Planet>().GenerateMesh(currentPlanet);
        }
    }
}
