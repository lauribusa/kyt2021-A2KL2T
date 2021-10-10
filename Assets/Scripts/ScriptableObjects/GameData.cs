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
	[Tooltip("Cooldown time for player vulnerability post-parry")]
	public float cooldownTime;
	[Header("Other things")]
	public int maxPlayers = 4;
	public int scoreToWin = 5;
	public float respawnTime = 5;

	[Header("Initial Map")]
	public GameObject[] maps;
}