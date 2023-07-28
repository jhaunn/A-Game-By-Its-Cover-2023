using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class PlantsSO : ScriptableObject
{
    public Sprite seed;
    public Sprite[] growth = new Sprite[4];
    public Sprite yield;
}
