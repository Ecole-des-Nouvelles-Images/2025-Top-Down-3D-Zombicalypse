using Julien.Script.Struc;
using Script;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Julien.Script.TurelScripts
{
    public class TurelOnGround : MonoBehaviour, IInteractable
    {
        public TurelWrap TurelWrap;
        private void Start()
        {
            TurelWrap.SetFirstData();
            SetVisual();
        }
        public void Activate(Player player)
        {
            Player P = player.GetComponent<Player>();
            Debug.Log("interact with Turel");
            
            player.GetComponent<InventoryPlayer>().TurelWrap = TurelWrap;
            
            P.handingObject.HandingWeapon.SetActive(false);
            P.handingObject.TurelPrefab = TurelWrap.Turel.VisualHologram;

            GameObject turelHologram = Instantiate(TurelWrap.Turel.VisualHologram, P.handingObject.HandingTurel.gameObject.transform.position, player.PlayerRenderer.transform.rotation, P.handingObject.HandingTurel.transform);
            Debug.Log(" player rotation = " + player.PlayerRenderer.transform.rotation);
            Debug.Log(" Turel rotation = " + turelHologram.transform.rotation);
            turelHologram.GetComponent<HologramTurel>().TurelWrap = TurelWrap;
            
            P.SwitchInputHandler(1);
            
            Destroy(gameObject);
        }
        
        public void SetVisual()
        {
            GameObject turel = Instantiate(TurelWrap.Turel.OnGroundPrefab, transform.position, Quaternion.identity, transform);
        }
    }
}
