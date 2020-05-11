using UnityEngine;

public class WorldGenerator
{
    // generate new planet shell
    public static PlanetData GeneratePlanet()
    {
        Object[] objs = Resources.LoadAll("Planets");
        int randomPlanetArchetype = Random.Range(0, objs.Length);

        PlanetArchetype planetType = objs[randomPlanetArchetype] as PlanetArchetype;
        PlanetData data = new PlanetData();

        data.size = Random.Range(PlanetData.minPlanetSize, PlanetData.maxPlanetSize);
        data.orbitRadius = Random.Range(PlanetData.minOrbitRadius, PlanetData.maxOrbitRadius);

        data.obstacleCount = Random.Range(planetType.obstacleCountBounds[0], planetType.obstacleCountBounds[1]);

        return data;
    }

    // generate new "world"
    public static StarSystemData GenerateStarSystem()
    {
        // get a random planet count
        int planetCount = new System.Random().Next(StarSystemData.minPlanetCount, StarSystemData.maxPlanetCount);
        PlanetData[] planets = new PlanetData[planetCount];

        for (int i = 0; i < planets.Length; i++)
        {
            planets[i] = GeneratePlanet();
        } 

        return new StarSystemData(planets);
    }
}
