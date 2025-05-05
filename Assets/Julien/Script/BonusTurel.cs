using System.Collections.Generic;
using Julien.Script.Data.Upgrader;
using Julien.Script.Interface;
using Julien.Script.Struc;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;

namespace Julien.Script
{
    public class BonusTurel : MonoBehaviour, IInteractable, IRandom
    {
        [SerializeField] private List<Upgrader> _types = new List<Upgrader>();
        
        public UpgraderWrap UpgraderWrap;
        private void Start()
        {
            
        }
        public void Random()
        {
            UpgraderWrap.UpgraderType = _types[UnityEngine.Random.Range(0, _types.Count)];
            UpgraderWrap.SetData();
            SetVisual();
        }
        public void Activate(Player player)
        {
            player.GetComponent<InventoryPlayer>().UpgraderWrap = UpgraderWrap;
            GameObject bonus = Instantiate(UpgraderWrap.UpgraderType.VisualGameObject, player.handingObject.HandingBonus.transform.position, player.handingObject.HandingBonus.transform.rotation, player.handingObject.HandingBonus.transform);
            player.handingObject.HandingWeapon.SetActive(false);
            player.GetComponent<Player>().SwitchInputHandler(2);
            Destroy(gameObject);
        }
        public void SetVisual()
        {
            Instantiate(UpgraderWrap.UpgraderType.VisualGameObject, transform.position, quaternion.identity, transform);
        }
    }
}
