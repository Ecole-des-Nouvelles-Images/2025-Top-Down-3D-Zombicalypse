using System;
using System.Collections;
using Julien.Script.PlayerScripts;
using Julien.Script.ZombieScript;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Julien.Script
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigibody;
        [SerializeField] private Player _player;
        
        public float BulletSpeed;
        public float DamageBullet;
        public float Precision;
        public float LethalRange;
        
        private void Start()
        {
            Impulse();
            StartCoroutine("DestroyDelay");
        }

        private IEnumerator DestroyDelay()
        {
            yield return new WaitForSeconds(LethalRange);
            
            Destroy(gameObject);
        }

        public void Impulse()
        {
            float randomDirection = Random.Range(-Precision, Precision);
            _rigibody.AddForce((gameObject.transform.forward + gameObject.transform.right * randomDirection) * BulletSpeed, ForceMode.Impulse);
        }

        public void SetBulletParameter(float bulletSpeed, float damage, float precision, float range, Player player)
        {
            BulletSpeed = bulletSpeed;
            DamageBullet = damage;
            Precision = precision;
            LethalRange = range;
            if (player != null) _player = player;
        }

        private void Update()
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward * 2 , Color.blue, 1f);
            if (Physics.Raycast(transform.position, transform.forward * 2 , out hit, 2))
            {
                if (hit.collider.CompareTag("Zombie"))
                {
                    if (_player != null) _player.GetComponent<PlayerScore>().DamageCount += DamageBullet;
                    Zombie zombie = hit.transform.gameObject.GetComponent<Zombie>();
                    zombie.Damaged(DamageBullet, _player);
                    Destroy(gameObject);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Zombie"))
            {
                if (_player != null) _player.GetComponent<PlayerScore>().DamageCount += DamageBullet;
                Zombie zombie = other.gameObject.GetComponent<Zombie>();
                zombie.Damaged(DamageBullet, _player);
                Destroy(gameObject);
            }
        }
    }
}
