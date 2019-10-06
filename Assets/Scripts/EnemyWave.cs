using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave", order = 51)]
public class EnemyWave : ScriptableObject
{
    public GameObject EnemyType;
    public int Number;
    public float Time;
}
