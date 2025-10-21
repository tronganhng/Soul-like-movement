using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class CharacterOS: ScriptableObject 
{
    public List<CharacterData> playerDatas;
}

[Serializable]
public class CharacterData
{
    public int ID;
    public string name;
    public GameObject prefab;
    public int level;
    public int maxHealth;
    public float speed;
    public float gravity;
    public float jumpForce;
}
