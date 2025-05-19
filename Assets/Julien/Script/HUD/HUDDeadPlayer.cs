using Julien.Script.PlayerScripts;
using Julien.Script.Static;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Julien.Script.HUD
{
    public class HUDDeadPlayer : MonoBehaviour
    {
        [SerializeField] private Player _playerTarget;
        [SerializeField] private float _timer;

        [SerializeField] private TMP_Text _timerText;
        
        public void SetInfo(Player player)
        {
            _playerTarget = player;
            _timer = player.timeBeforRespawn;
            SetPosition();
            _playerTarget.gameObject.SetActive(false);
        }
        
        public void SetPosition()
        {
            RectTransform hudRectTransform = GetComponent<RectTransform>();

            int index = _playerTarget.PlayerIndex - 1;
            
            hudRectTransform.anchorMin = new Vector2(GameManagerStatic.Anchors[index].x, GameManagerStatic.Anchors[index].y);
            hudRectTransform.anchorMax = new Vector2(GameManagerStatic.Anchors[index].z, GameManagerStatic.Anchors[index].w);
            hudRectTransform.offsetMin = new Vector2(0, 0);
            hudRectTransform.offsetMax = new Vector2(0, 0);
        }

        private void Update()
        {
            _timer -= Time.deltaTime;
            _timerText.text = Mathf.RoundToInt(_timer).ToString();
            if (_timer <= 0)
            {
                _playerTarget.gameObject.SetActive(true);
                _playerTarget.Respawn();
                Destroy(gameObject);
            }
        }
    }
}
