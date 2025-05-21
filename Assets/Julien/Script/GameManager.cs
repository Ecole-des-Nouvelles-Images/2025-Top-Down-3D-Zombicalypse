using Julien.Script.Static;
using Unity.AI.Navigation;
using UnityEngine;

namespace Julien.Script
{
    public class GameManager : MonoBehaviour
    {
        public float CurrentPoint;
        public float MaxProgressBar;
        
        [SerializeField] private GameObject _weaponPrefab;
        [SerializeField] private GameObject _parentSpawn;

        [SerializeField] private NavMeshSurface _navMeshSurface;

        [ContextMenu("SpawnWeapon")]
        public void SpawnWeapon()
        {
            BoxCollider box = _parentSpawn.GetComponent<BoxCollider>();
            
            float x = box.center.x + box.size.x / 2;
            float z = box.center.z + box.size.z / 2;
            
            float RandomX = Random.Range(-x, x);
            float RandomZ = Random.Range(-z, z);

            GameObject ObjectToSpawn = Instantiate(_weaponPrefab, new Vector3(GameManagerStatic.lastZombiePosition.x, GameManagerStatic.lastZombiePosition.y, GameManagerStatic.lastZombiePosition.z), Quaternion.identity, _parentSpawn.transform);
        }

        [ContextMenu("Bake")]
        public void BakeGround()
        {
            _navMeshSurface.BuildNavMesh();
        }
    }
}
