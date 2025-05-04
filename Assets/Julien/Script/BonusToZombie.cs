using System.Collections.Generic;
using Julien.Script;
using UnityEngine;

namespace Script
{
    public class BonusToZombie : MonoBehaviour
    {
        
        public float MultiplyStat;
        public List<float> MutilplyValues;
        public int NumberRound;
        
        public List<float> Bonus;
        private RoundHundler _roundHundler;
        
        private void Start()
        {
            _roundHundler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RoundHundler>();
            
            for (int i = 0; i < NumberRound; i++)
            {
                if (i <= 50)
                {
                    float value =  MultiplyStat *= MutilplyValues[0];
                    Bonus.Add(value);
                }
                else if (i <= 100)
                {
                    float value =  MultiplyStat *= MutilplyValues[1];
                    Bonus.Add(value);
                }
                else if (i <= 150)
                {
                    float value =  MultiplyStat *= MutilplyValues[2];
                    Bonus.Add(value);
                }
                else if (i > 200)
                {
                    float value =  MultiplyStat *= MutilplyValues[3];
                    Bonus.Add(value);
                }
            }
        }
    }
}
