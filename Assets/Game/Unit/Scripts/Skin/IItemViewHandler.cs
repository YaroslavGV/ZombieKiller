using UnityEngine;

public interface IItemViewHandler
{
    T SpawnView<T> (T template, string point) where T : Object;
    void RemoveView (Object view);
    Transform GetWeaponPoint ();
}
