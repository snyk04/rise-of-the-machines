﻿ using UnityEngine;

 namespace Project.Classes.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObject/Weapon", order = 0)]
    public class WeaponSO : ScriptableObject
    {
        public const float DEFAULT_SPREAD_DISTANCE = 100f;

        [Header("Naming")]
        public string name;
        public int weaponID;

        [Header("Characteristics")]
        public float damage;
        [Range(0, 1)] public float damageSpread;
        public int maxBulletsInMagazine;
        public int maxAllAmmo;
        public int currentBulletsInMagazine;
        public int allAmmo; 
        public int bulletsPerShot = 1;
        public float reloadTime;
        public float shotSpread; // range fo spread after DEFAULT_SPREAD_DISTANCE
        public float shotsPerSecond;
        public float maxShotDistance;
        public bool isAutomatic;

        [Header("Sound")]
        public AudioClip shotSound;
        public AudioClip noAmmoSound;
        public AudioClip reloadSound;

        [Header("Other")]
        // public WeaponSlot.Spot spot;
        public bool isReloading;
    }
}