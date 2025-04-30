using System;
using Script.Data.WeaponsData;

namespace Script.Struc
{
    [Serializable]
    public struct WeaponWrap
    {
        public Weapon Weapon;
        public int CurrentAmmo;
        public int CurrentMagazin;
        public void SetFirstData()
        {
            CurrentAmmo = Weapon.MaxAmmo;
            CurrentMagazin = Weapon.MaxMagazine;
        }

        public void ClearData()
        {
            Weapon = null;
            CurrentAmmo = 0;
            CurrentMagazin = 0;
        }
    }
}
