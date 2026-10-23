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
                    // Change color skin
                    foreach (GameObject cloth in  player.Cloths)
                    {
                        Material copieMaterial = new Material(cloth.GetComponent<Renderer>().material);
                        cloth.GetComponent<Renderer>().sharedMaterial = copieMaterial;
                        copieMaterial.color = _colors[index];
                        cloth.GetComponent<Renderer>().material = copieMaterial;
                    }


                    // change laser color
                    LineRenderer lineRenderer = player.GetComponentInChildren<LineRenderer>();
                    if (lineRenderer != null) lineRenderer.material.color = _colors[index];
                    
                    player.PlayerColor = _colors[index];

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
