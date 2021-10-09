using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 1)]
public class GameData : ScriptableObject
{
	[Header("Timers")]
	[Tooltip("Initial timer on game start.")]
	public int initialTimer;
	[Header("Initial Map")]
	public GameObject initialMap;
}

