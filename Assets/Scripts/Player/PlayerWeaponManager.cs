using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponPair
{
    public string weaponName;
    public Weapon weaponPrefab;
}

public class PlayerWeaponManager : MonoBehaviour
{
    public List<WeaponPair> availableWeapons;
    private List<Weapon> instantiatedWeapons = new List<Weapon>();

    public List<Weapon> InstantiatedWeapons { get => instantiatedWeapons; set => instantiatedWeapons = value; }

    public void InstantiateAllWeapons(Transform parent)
    {
        foreach (var pair in availableWeapons)
        {
            Weapon newWeaponInstance = InstantiateWeapon(pair.weaponPrefab, parent);
            InstantiatedWeapons.Add(newWeaponInstance);
        }
    }

    public void InstantiateWeaponsByNameList(List<string> weaponNames, Transform parent)
    {
        foreach (var name in weaponNames)
        {
            WeaponPair pair = availableWeapons.Find(wp => wp.weaponName == name);
            if (pair != null)
            {
                Weapon newWeaponInstance = InstantiateWeapon(pair.weaponPrefab, parent);
                InstantiatedWeapons.Add(newWeaponInstance);
            }
            else
            {
                Debug.LogWarning("Weapon not found: " + name);
            }
        }
    }

    public void DisableWeaponRendering()
    {
        foreach (var weapon in InstantiatedWeapons)
        {
            weapon.gameObject.SetActive(false);
        }
    }

    private Weapon InstantiateWeapon(Weapon weaponPrefab, Transform parent)
    {
        Weapon newWeaponInstance = Instantiate(weaponPrefab, parent);

        newWeaponInstance.transform.parent = parent;

        return newWeaponInstance;
    }
}
