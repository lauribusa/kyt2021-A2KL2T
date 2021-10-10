using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
	#region Exposed

    [SerializeField]
    private AudioClip[] _audioCrowdHappyClips;

    [SerializeField]
    private AudioClip[] _audioPinballClips;
	
	#endregion
	
	
   	#region Private And Protected

    private AudioSource _audioSource;
   	
   	#endregion
	
	
	#region Unity API
	
    private void Start()
    {
        if (_audioSource == null) _audioSource = GetComponent<AudioSource>();
    }
    
    #endregion


    #region Main

    public void PlayHappyCrowd()
    {
        PlayRandomClip(_audioCrowdHappyClips);
    }

    public void PlayPinball()
    {
        PlayRandomClip(_audioPinballClips);
    }
    
    #endregion
    
    
    #region Utils

    private void PlayRandomClip(AudioClip[] audioClips)
    {
        int randomIndex = Random.Range(0, audioClips.Length);
        _audioSource.PlayOneShot(audioClips[randomIndex]);
    }
    
    #endregion
}