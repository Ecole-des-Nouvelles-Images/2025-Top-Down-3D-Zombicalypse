using System.Collections;
using Julien.Script.PlayerScripts;
using Julien.Script.ZombieScript;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
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

        public void SetBulletParameter(float bulletSpeed, float damage, float precision, float range)
        {
            BulletSpeed = bulletSpeed;
            DamageBullet = damage;
            Precision = precision;
            LethalRange = range;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Zombie"))
            {
                Zombie zombie = other.gameObject.GetComponent<Zombie>();
                zombie.Damaged(DamageBullet);
                Destroy(gameObject);
            }
        }
    }
}
