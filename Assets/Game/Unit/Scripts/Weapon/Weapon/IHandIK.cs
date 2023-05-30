namespace Weapon
{
    public interface IHandIK
    {
        TargetIK FrontHandIK { get; }
        TargetIK BackHandIK { get; }
    }
}