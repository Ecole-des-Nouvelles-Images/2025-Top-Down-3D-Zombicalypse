using Julien.Script.Interface;
using Julien.Script.PlayerScripts;
using Unity.Mathematics;
using UnityEngine;

namespace Julien.Script
{
    public class HealthKit : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _healthAreaPrefab;


        public void Activate(Player player)
        {
            Instantiate(_healthAreaPrefab, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), quaternion.identity);
            Destroy(gameObject);
        }
    }
}
