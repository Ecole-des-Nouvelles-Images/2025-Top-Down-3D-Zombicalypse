using Julien.Script.TurelScripts.State;
using Script.Turel.State;

namespace Julien.Script.TurelScripts
{
    public class TurelNormal : Julien.Script.TurelScripts.Turel
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
