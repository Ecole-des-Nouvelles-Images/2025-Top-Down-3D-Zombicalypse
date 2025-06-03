using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Julien.Script
{
    public class SoundManager : MonoBehaviourSingleton<SoundManager>
    {
        [Header("Player")]
        public List<AudioClip> StepsPlayer = new List<AudioClip>();
        public List<AudioClip> DamagedPlayer = new List<AudioClip>();
        public List<AudioClip> DeadPlayer = new List<AudioClip>();
        public List<AudioClip> RespawnPlayer = new List<AudioClip>();
        
        [Header("Zombie")]
        public List<AudioClip> AttackZombie = new List<AudioClip>();
        public List<AudioClip> StepsZombie = new List<AudioClip>();
        public List<AudioClip> DeadZombie = new List<AudioClip>();
        public List<AudioClip> ScreamZombie = new List<AudioClip>();
        
        [Header("Turel")]
        public List<AudioClip> TurelShoot = new List<AudioClip>();
        public List<AudioClip> TurelDestroy = new List<AudioClip>();
        
        [Header("Generator")]
        public List<AudioClip> Repart = new List<AudioClip>();
        public List<AudioClip> Break = new List<AudioClip>();
        
        [Header("WaponsSound")]
        public List<AudioClip> Ak47 = new List<AudioClip>();
        public List<AudioClip> Spas = new List<AudioClip>();
        public List<AudioClip> Colt = new List<AudioClip>();
        
        [Header("HitSoundZombie")]
        public List<AudioClip> Hit = new List<AudioClip>();
        
        /// <summary>
        /// l'obj est le gameobject ou serra instancier l'audio source. Clips est une list d'audioClip qui est disponible dans le soundManager
        /// ( l'audioClip se surpimera une fois le son fini )
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="clips"></param>
        public void PlaySound(GameObject obj, List<AudioClip> clips)
        {
           AudioSource audio = obj.AddComponent<AudioSource>();
           audio.clip = clips[Random.Range(0, clips.Count)];
           audio.Play();
           float durationClip = audio.clip.length;
           StartCoroutine(RemoveAudioSource(durationClip, audio));
        }
        public IEnumerator RemoveAudioSource(float timer, AudioSource source)
        {
            yield return new WaitForSeconds(timer);
            Destroy(source);
        }
    }
}
