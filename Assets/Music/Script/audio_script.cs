using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class audio_script : MonoBehaviour {


	public AudioMixer audio_mixer;

	public bool channel_1;
	public float default_pitch = 1.0f;
    public float pitch;
	
	public bool stop;


	public float fadeout_speed = 10.0f;
	public float fadein_speed = 30.0f;

	private AudioSource audio_channel_1;






	private float audio_channel_1_vol = -80.0f;




	private Object[] AudioArray_channel_1;






	// Use this for initialization
	void Start () {

		audio_channel_1 = (AudioSource)gameObject.AddComponent <AudioSource>();




		AudioArray_channel_1 = Resources.LoadAll("1",typeof(AudioClip));




		audio_channel_1.outputAudioMixerGroup = audio_mixer.FindMatchingGroups("channel_1")[0];




		audio_channel_1.clip = AudioArray_channel_1[0] as AudioClip;




		audio_channel_1.loop = true;




		audio_channel_1.Play();




	}
	
	// Update is called once per frame
	void Update () {
		SetVolumes ();
		Pitch_Set ();
		if (stop) {
			StopAllMusic ();
		}

	}

	public void StopAllMusic(){
		channel_1 = false;

		if (audio_channel_1_vol < -79.0f) {
			audio_channel_1.Stop ();
		}
	

	}

	public void Pitch_Set(){
		audio_channel_1.pitch = default_pitch * pitch;
	}

	public void SetVolumes(){
		audio_mixer.SetFloat ("channel_1", audio_channel_1_vol);




		if (channel_1) {
			if (audio_channel_1_vol < 0.0f) {
				audio_channel_1_vol += fadein_speed * Time.deltaTime;	
			}
		}
		if (!channel_1) {
			if (audio_channel_1_vol > -80.0f) {
				audio_channel_1_vol -= fadeout_speed * Time.deltaTime;	
			}

		}




	}

	public void StopMusic(){
		stop = true;
	}

	

}
