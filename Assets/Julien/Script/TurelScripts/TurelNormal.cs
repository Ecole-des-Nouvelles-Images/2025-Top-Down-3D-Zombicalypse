using Script.Turel.State;

namespace Script.Turel
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
