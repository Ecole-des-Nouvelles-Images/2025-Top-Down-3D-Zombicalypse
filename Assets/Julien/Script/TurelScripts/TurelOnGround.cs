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

            GameObject turelHologram = Instantiate(TurelWrap.Turel.VisualHologram, P.handingObject.HandingTurel.gameObject.transform.position, quaternion.identity, P.handingObject.HandingTurel.transform);
            
            P.SwitchInputHandler(1);
            
            
            Destroy(gameObject);
        }
        
        public void SetVisual()
        {
            GameObject turel = Instantiate(TurelWrap.Turel.Prefab, transform.position, Quaternion.identity, transform);
            turel.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
        }
    }
}
