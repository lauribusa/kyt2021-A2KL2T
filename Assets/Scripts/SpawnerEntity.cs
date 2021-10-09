using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnerMode
{
    Player1,
    Player2,
    Player3,
    Player4,
    Ball
}

public class SpawnerEntity : MonoBehaviour
{
    public SpawnerMode spawnerMode;
}
