namespace Weapon
{
    public interface IAmmoUser
    {
        public AmmoType UsesAmmo { get; }

        public void SetAmmoBackpack (IAmmoBackpack backpack);
    }
}