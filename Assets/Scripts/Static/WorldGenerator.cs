using UnityEngine;

public class WorldGenerator
{
    // generate new planet shell
    public static PlanetData GeneratePlanet()
    {
        int planetSize = Random.Range(PlanetData.minPlanetSize, PlanetData.maxPlanetSize);
        int orbitRadius = Random.Range(PlanetData.minOrbitRadius, PlanetData.maxOrbitRadius);

        return new PlanetData(planetSize, orbitRadius);
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
