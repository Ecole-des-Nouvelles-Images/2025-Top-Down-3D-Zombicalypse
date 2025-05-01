using Julien.Script;
using Script.Struc;
using UnityEngine;

namespace Script.Turel
{
    public class TurelOnGround : MonoBehaviour, IInteractable
    {
        public TurelWrap turelWrap;
        private void Start()
        {
            turelWrap.SetFirstData();
            SetVisual();
        }
        public void Activate(Player player)
        {
            Debug.Log("interact with Turel");
            player.GetComponent<InventoryPlayer>().TookTurel(turelWrap, turelWrap.Turel.Visual);
            player.GetComponent<Player>().handingObject.TurelMesh = turelWrap.Turel.Visual;
            Destroy(gameObject);
        }
        
        public void SetVisual()
        {
            GameObject turel = Instantiate(turelWrap.Turel.Visual, transform.position, Quaternion.identity, transform);
            turel.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
        }
    }
}
