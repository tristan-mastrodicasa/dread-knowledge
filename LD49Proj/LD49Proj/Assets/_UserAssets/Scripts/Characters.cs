using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters
{
    // Ordered list of all characters.
    public static List<Character> characters;
}

public class Character {

    // Name
    public string characterName;

    // Appearance / which character model this character has enabled.
    public int modelIndex;

    // 1-6 scale
    public int sanity;

    // 1-6 scale
    public int health;


}
