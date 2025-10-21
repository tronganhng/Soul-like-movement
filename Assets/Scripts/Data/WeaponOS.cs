using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class WeaponOS : ScriptableObject
{
    public List<Weapon> weapons;
}

[Serializable]
public class Weapon
{
    public Weapons name;
    public int level;
    public int dame;
    public RuntimeAnimatorController animatorController;
    public GameObject prefab;
}
