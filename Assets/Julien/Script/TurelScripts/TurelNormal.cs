using Julien.Script.TurelScripts.State;
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
            CurrentStat.Execute(this);
        }
    }
}
