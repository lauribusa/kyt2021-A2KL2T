using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 1)]
public class GameData : ScriptableObject
{
	[Header("Timers")]
	[Tooltip("Initial timer on game start.")]
	public int initialTimer;
	[Tooltip("Time in deltaTime for parrying time frame")]
	public float parryingTime;

	[Header("Initial Map")]
	public GameObject[] maps;
}