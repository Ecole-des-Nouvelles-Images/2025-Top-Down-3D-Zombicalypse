using System.Collections.Generic;
using Julien.Script.Interface;
using Julien.Script.PlayerScripts;
using Julien.Script.Struc;
using Script.Data.TurellData;
using UnityEngine;

namespace Julien.Script.TurelScripts
{
    public class TurelOnGround : MonoBehaviour, IInteractable, IRandom
    {
        [SerializeField] private List<TurelData> _turelsData = new List<TurelData>();
        
        public TurelWrap TurelWrap;
        private void Start()
        {
            // Random();
            // SetVisual();
        }
        public void Activate(Player player)
        {
            Player P = player.GetComponent<Player>();
            
            player.GetComponent<InventoryPlayer>().TurelWrap = TurelWrap;
            
            P.handingObject.HandingWeapon.SetActive(false);
            P.handingObject.TurelPrefab = TurelWrap.TurelType.VisualHologram;

            GameObject turelHologram = Instantiate(TurelWrap.TurelType.VisualHologram, P.handingObject.HandingTurel.gameObject.transform.position, player.PlayerRenderer.transform.rotation, P.handingObject.HandingTurel.transform);
            turelHologram.GetComponent<HologramTurel>().TurelWrap = TurelWrap;
            
            P.SwitchInputHandler(1);
            
            Destroy(gameObject);
        }
        public void Random()
        {
            TurelWrap.TurelType = _turelsData[UnityEngine.Random.Range(0, _turelsData.Count)];
            TurelWrap.SetFirstData();
            SetVisual();
        }
        public void SetVisual()
        {
            GameObject turel = Instantiate(TurelWrap.TurelType.OnGroundPrefab, transform.position, Quaternion.identity, transform);
        }
    }
}
