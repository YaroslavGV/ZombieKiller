using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Unit.Skin
{
    [RequireComponent(typeof(Animator), typeof(Collider2D))]
    public class UnitSkin : UnitModul, IItemViewHandler
    {
        public event Action AttackHitCallback;
        [SerializeField] private string _key;
        [SerializeField] private Point[] _points;
        [SerializeField] private Key _weaponPoint;
        [Space]
        [SerializeField] private UnitSkinModul[] _moduls;
        
        public string Key => _key;
        public IEnumerable<Point> Points => _points;
        public Animator Animator { get; private set; }
        public Collider2D Collider { get; private set; }

        public override void Tick ()
        {
            foreach (UnitSkinModul modul in _moduls)
                modul.Tick();
        }

        public override void LateTick ()
        {
            foreach (UnitSkinModul modul in _moduls)
                modul.LateTick();
        }

        public override void OnDead ()
        {
            foreach (UnitSkinModul modul in _moduls)
                modul.OnDead();
        }

        public T SpawnView<T> (T template, string point) where T : UnityEngine.Object
        {
            Transform parent = GetPoint(point);
            T view = Instantiate(template, parent);
            foreach (UnitSkinModul modul in _moduls)
                modul.OnSpawnView(view);
            return view;
        }

        public void RemoveView (UnityEngine.Object view)
        {
            foreach (UnitSkinModul modul in _moduls)
                modul.OnRemoveView(view);
            if (view is GameObject)
                Destroy(view);
            if (view is Component b)
                Destroy(b.gameObject);
        }

        public Transform GetPoint (string pointName)
        {
            Point point = _points.FirstOrDefault(p => p.key.Name == pointName);
            if (point.transform != null)
                return point.transform;
            Debug.LogWarning(string.Format("{0}: point {1} is missing", name, pointName));
            return transform;
        }

        public Transform GetWeaponPoint () => GetPoint(_weaponPoint.Name);

        public bool HasPoint (string pointName)
        {
            Point point = _points.FirstOrDefault(p => p.key.Name == pointName);
            return point.transform != null;
        }

        public void AttackHitEvent () => AttackHitCallback?.Invoke();

        protected override void OnInitialize ()
        {
            Animator = GetComponent<Animator>();
            Collider = GetComponent<Collider2D>();
            foreach (UnitSkinModul modul in _moduls)
                modul.Initialize(Unit);
        }
    }
}