using System;
using UnityEngine;

// what a star system is

[Serializable]
public class StarSystemData
{
    public static int minPlanetCount = 5;
    public static int maxPlanetCount = 10;

    PlanetData[] planets;
    public PlanetData[] GetPlanets() { return planets; }

    public StarSystemData(PlanetData[] planets)
    {
        this.planets = planets;
    }

}
