using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAudio : MonoBehaviour
{
    // 0 = roam, 1 = agro, 2 = howl 
    //GetComponent<WolfAudio>().Walk((temp.magnitude > 0.01f)? true:false);
    //GetComponent<WolfAudio>().PlaySound(1);
    [SerializeField] AudioClip[] sounds;
    
    AudioSource[] s;

    private void Start() {
        s = GetComponents<AudioSource>();
    }

    public void Walk(bool walking){
        /* if(walking){
            if(!s[0].isPlaying){
                s[0].Play(0);
            }
        }
        else if (s[0].isPlaying){s[0].Stop();} */
    }

    public void PlaySound(int index){
        if(index != 2){s[1].loop = true;}
        else {s[1].loop = false;}
        s[1].clip = sounds[index];
        s[1].Play(0);
    }
}
