using Julien.Script.Struc;
using Script;
using Unity.Mathematics;
using UnityEngine;

namespace Julien.Script
{
    public class BonusTurel : MonoBehaviour, IInteractable
    {
        public UpgraderWrap UpgraderWrap;
        private void Start()
        {
            UpgraderWrap.SetData();
            SetVisual();
        }
        
        public void SetVisual()
        {
            Instantiate(UpgraderWrap.Upgrader.VisualGameObject, transform.position, quaternion.identity, transform);
        }
        
        public void Activate(Player player)
        {
            player.GetComponent<InventoryPlayer>().UpgraderWrap = UpgraderWrap;
            GameObject bonus = Instantiate(UpgraderWrap.Upgrader.VisualGameObject, player.handingObject.HandingBonus.transform.position, player.handingObject.HandingBonus.transform.rotation, player.handingObject.HandingBonus.transform);
            player.handingObject.HandingWeapon.SetActive(false);
            player.GetComponent<Player>().SwitchInputHandler(2);
            Destroy(gameObject);
        }
    }
}
