using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
     // Declare all clip names
    public static AudioClip placement, pickup, turn, win, loss;
    static AudioSource audioSrc;
    void Start()
    {
    	//all audio clips can be found in the resources folder
    	//exampleName = Resources.Load<AudioClip> ("ActualAudioClipName");
    	placement = Resources.Load<AudioClip> ("BlockPlace");
    	pickup = Resources.Load<AudioClip> ("BlockPick");
    	turn = Resources.Load<AudioClip> ("Switch");
    	win = Resources.Load<AudioClip> ("Fanfare");
    	loss = Resources.Load<AudioClip> ("ReverseFanfare");
    	audioSrc = GetComponent<AudioSource> ();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
    	switch (clip)
    	{
    		case "MetalTink":
    			audioSrc.PlayOneShot(placement);
    			break;
    		case "Swoosh":
    			audioSrc.PlayOneShot(pickup);
    			break;
    		case "Bloop":
    			audioSrc.PlayOneShot(turn);
    			break;
    		case "Victory":
    			audioSrc.PlayOneShot(win);
    			break;
    		case "Defeat":
    			audioSrc.PlayOneShot(loss);
    			break;
    	}
    }
}
