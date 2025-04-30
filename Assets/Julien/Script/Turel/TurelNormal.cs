using System;
using Script.Turel.State;
using UnityEngine;

namespace Script.Turel
{
    public class TurelNormal : Turel
    {
        private void Update()
        {
            switch (HaveTarget)
            {
                case false:
                    CurrentStat = new TurelShearch();
                    break;
                case true:
                    CurrentStat = new TurelShoot();
                    break;
            }
            
            CurrentStat.Execute(this);
        }
    }
}
