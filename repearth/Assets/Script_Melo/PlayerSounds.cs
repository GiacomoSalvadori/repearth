using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Player Sounds", fileName = "Player Sounds")]
public class PlayerSounds : ScriptableObject
{
    public SoundClass[] personalEffect;
}