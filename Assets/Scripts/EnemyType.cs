using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Type" ,menuName = "Enemy Type")]

public class EnemyType : ScriptableObject
{
    public string enemyName = "Wizard";
    public int enemyMaxHealth = 80;
}
