using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
   	#region Private And Protected

    private ParticleSystem _particle;
   	
   	#endregion
	
	
	#region Unity API
	
    private void Start()
    {
        if (_particle == null) _particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (_particle.isPlaying) return;
        Debug.Log("destroy");
        Destroy(gameObject);
    }
    
    #endregion
}