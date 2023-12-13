using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponPair
{
    public string weaponName;
    public PlayerWeapon weaponPrefab;
}

public class PlayerWeaponManager : MonoBehaviour
{
    public List<WeaponPair> availableWeapons;
    private List<PlayerWeapon> instantiatedWeapons = new();

    public List<PlayerWeapon> InstantiatedWeapons { get => instantiatedWeapons; set => instantiatedWeapons = value; }

    public void InstantiateAllWeapons(Transform parent)
    {
        foreach (var pair in availableWeapons)
        {
            PlayerWeapon newWeaponInstance = InstantiateWeapon(pair.weaponPrefab, parent);
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
                PlayerWeapon newWeaponInstance = InstantiateWeapon(pair.weaponPrefab, parent);
                InstantiatedWeapons.Add(newWeaponInstance);
            }
            else
            {
                Debug.LogWarning("PlayerWeapon not found: " + name);
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

    private PlayerWeapon InstantiateWeapon(PlayerWeapon weaponPrefab, Transform parent)
    {
        PlayerWeapon newWeaponInstance = Instantiate(weaponPrefab, parent);

        newWeaponInstance.transform.parent = parent;

        return newWeaponInstance;
    }
}
