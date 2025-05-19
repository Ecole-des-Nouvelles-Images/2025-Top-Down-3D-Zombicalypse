using Julien.Script.TurelScripts.State;
using Script.Turel.State;
using UnityEngine;

namespace Julien.Script.TurelScripts
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
            Debug.Log(CurrentStat);
            CurrentStat.Execute(this);
        }
    }
}
