using Classes.TryInHierarchie;
using UnityEngine;

namespace Classes.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObject/Weapon", order = 0)]
    public class WeaponSO : ScriptableObject {
        public const float DEFAULT_SPREAD_DISTANCE = 100f;
        
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
        public float spread; // range fo spread after DEFAULT_SPREAD_DISTANCE
        public AudioClip reloadSound;
        public float fireRate; // shoots in second
        public bool isAutomatic;
        public WeaponSlot.Spot spot;
        public int amountOfBullets = 1; // bullets per shot
        public float maxDistance; // bullets per shot
    }
}