using System;
using System.Collections.Generic;
using Julien.Script.Interface;
using Julien.Script.PlayerScripts;
using UnityEngine;

namespace Julien.Script.Lobby
{
    public class ChangeColorPlayer : MonoBehaviour, IInteractable
    {
        [SerializeField] private List<Color> _colors = new List<Color>();
        [SerializeField] private Dictionary<Player, int> _dictionaryPlayer = new Dictionary<Player, int>();
        
        
        public void Activate(Player player)
        {
            if (!_dictionaryPlayer.ContainsKey(player))
            {
                _dictionaryPlayer.Add(player, 0);
            }

            if (_dictionaryPlayer.TryGetValue(player, out int index))
            {
                if (index >= 0 && index < _colors.Count)
                {
                    foreach (GameObject cloth in  player.GetComponent<Player>().Cloths)
                    {
                        Debug.Log("Change l'habit de couleur");
                       // cloth.GetComponent<MeshRenderer>().material.color = //_colors[index];
                    }
                    //player.GetComponent<Player>().PlayerRenderer.GetComponent<MeshRenderer>().material.color = _colors[index];
                    _dictionaryPlayer[player] += 1;
                    if ( _dictionaryPlayer[player] == _colors.Count)
                    {
                        _dictionaryPlayer[player] = 0;
                    }
                }
            }
        }
    }
}
