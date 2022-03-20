using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//currently broken!
namespace Prototype2
{
    [System.Serializable] //unlike a scriptable object it acts as part of a component of the script that's referencing it. A template that doesnt store data;
    public class Sound
    {
        //Clip info
        public string name;
        public AudioClip clip;
        //Volume settings
        [Range(0f, 1f)]
        public float volume = 0.7f;
        [Range(0.5f, 1.5f)]
        public float pitch = 1f;
        //Randomiser Range
        [Range(0, 0.5f)]
        public float randomVolume = 0.1f;
        [Range(0, 0.5f)]
        public float randomPitch = 0.1f;

        //Audio source Reference
        private AudioSource source;
        //Sets clip to be played
        public void SetSource (AudioSource _source)
        {
            source = _source;
            source.clip = clip;
        }
        //Plays the clip with some slight randomization
        public void Play()
        {
            source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
            source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
            source.Play();
        }
    }
    public class AudioManager : GameBehaviour
    {
        [SerializeField] // contains a list of sounds to call upon
        Sound[] sounds;

        private void Start()
        {
            for (int i=0; i<sounds.Length; i++)
            {
                //creates a gameobject for each sound and parents it to the game manager
                GameObject _go = new GameObject("Sound" + i + "_" + sounds[i].name);
                _go.transform.SetParent(this.transform);
                sounds[i].SetSource(_go.AddComponent<AudioSource>());
            }
            PlaySound("GobForest");

        }
        //Plays the sound when called upon;
        public void PlaySound(string _name)
        {
            for (int i=0; i<sounds.Length; i++)
            {
                if(sounds[i].name == _name)
                {
                    Debug.Log("sound found");
                    sounds[i].Play(); // is not playing for some reason
                    return;
                }
            }
            //no sound with _name
            Debug.LogWarning("AudioManager: Sound not found in list: " + _name);
        }
    }
}

