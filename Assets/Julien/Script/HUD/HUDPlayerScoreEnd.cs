using Julien.Script.PlayerScripts;
using Julien.Script.Static;
using TMPro;
using UnityEngine;

namespace Julien.Script.HUD
{
    public class HUDPlayerScoreEnd : MonoBehaviour
    {
        [SerializeField] private Player _playerTarget;
        [SerializeField] private PlayerScore _playerScore;

        [SerializeField] private TMP_Text _killCount;
        [SerializeField] private TMP_Text _damageCount;
        [SerializeField] private TMP_Text _deathCount;
        [SerializeField] private TMP_Text _totalScore;
        public void SetOnStart(Player player)
        {
            _playerTarget = player;
            _playerScore = player.GetComponent<PlayerScore>();
            SetPosition();
        }
        
        public void SetPosition()
        {
            RectTransform hudRectTransform = GetComponent<RectTransform>();

            int index = _playerTarget.PlayerIndex - 1;
            
            hudRectTransform.anchorMin = new Vector2(GameManagerStatic.AnchorsScore[index].x, GameManagerStatic.AnchorsScore[index].y);
            hudRectTransform.anchorMax = new Vector2(GameManagerStatic.AnchorsScore[index].z, GameManagerStatic.AnchorsScore[index].w);
            hudRectTransform.offsetMin = new Vector2(0, 0);
            hudRectTransform.offsetMax = new Vector2(0, 0);
        }

        [ContextMenu("SetScore")]
        public void OnEnable()
        {
            _playerScore.CalculScore();
            
            _killCount.text = _playerScore.ZombieKilled.ToString();
            _damageCount.text = _playerScore.DamageCount.ToString();
            _deathCount.text = _playerScore.DeadCount.ToString();
            _totalScore.text = _playerScore.Score.ToString();
            
        }
    }
}
