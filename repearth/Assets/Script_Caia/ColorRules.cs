using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorRules", menuName = "Scripting/ColorRules", order = 3)]
public class ColorRules : ScriptableObject
{
    public List<ColorRel> rels;

    public bool CheckStrenght(StateColor color, StateColor other)
    {
        ColorRel rel = rels.Find(x => x.color == color);
        
        return rel.strongWith.Contains(other);
    }

    public Color RetrieveHex(StateColor color)
    {
        ColorRel rel = rels.Find(x => x.color == color);

        return rel.colorHex;
    }
}

[System.Serializable]
public struct ColorRel
{
    public StateColor color;
    public Color colorHex;
    public List<StateColor> strongWith;
}
