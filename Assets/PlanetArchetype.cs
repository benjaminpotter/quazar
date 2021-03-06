﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Planet Archetype", menuName = "Planet Archetype", order = 0)]
public class PlanetArchetype : ScriptableObject
{
    public string planetName = "Planet Name";

    public int[] obstacleCountBounds;
}
