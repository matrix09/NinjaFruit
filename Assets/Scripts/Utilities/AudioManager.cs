using UnityEngine;
using AttTypeDefine;
public class AudioManager { 

    public static void PlayAudio (GameObject sourceObj, eAudioType type,string name, float _volumn = -1)
    {
        string route = "";
        switch(type)
        {
            case eAudioType.Audio_BackGround:
                {
                    route = "Audios/Background/" + name;
                    sourceObj = Camera.main.gameObject;
                    break;
                }
            case eAudioType.Audio_CutFruit:
                {
                    route = "Audios/Skills/" + name;
                    break;
                }
            default:
                {
                    Debug.LogError("error audio type = " + type);
                    return;
                }
              
        }

        AudioClip ac = Resources.Load(route) as AudioClip;


        AudioSource audioSrc = sourceObj.GetOrAddComponent<AudioSource>();
        audioSrc.clip = ac;
        audioSrc.Stop();
        audioSrc.playOnAwake = true;
        audioSrc.loop = false;

        if (_volumn > 0)
        {
            audioSrc.volume = _volumn;
        }
        else
            audioSrc.volume = 1f;

        audioSrc.Play();

    }
}
