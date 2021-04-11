using Classes.TryInHierarchie;
using UnityEngine;

namespace Classes.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObject/Weapon", order = 0)]
    public class WeaponSO : ScriptableObject
    {
        public string name;
        public int weaponID;
        public float damage;
        public int bulletsInMagazine;
        public int numOfBullets;
        public float reloadTime;
        public int currentAmmo;
        public bool isReloading;
        public AudioClip shootSound;
        public AudioClip noAmmoSound;
        public float spread; // range fo spread after 100 m
        public AudioClip reloadSound;
        public float fireRate; // shoots in second
        public bool isAutomatic;
        public WeaponSlot.Type type;
    }
}