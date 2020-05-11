using System;

[Serializable]
public class PlanetData
{
    public static int minPlanetSize = 3;
    public static int maxPlanetSize = 10;

    public static int minOrbitRadius = 20;
    public static int maxOrbitRadius = 100;

    public int orbitRadius { get; private set; }
    int size;

    public int GetRadius() { return size; }

    public PlanetData(int size, int orbitRadius)
    {
        this.orbitRadius = orbitRadius;
        this.size = size;
    }
}
