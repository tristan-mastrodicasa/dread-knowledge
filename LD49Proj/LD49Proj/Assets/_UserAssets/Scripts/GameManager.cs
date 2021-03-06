using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Don't Destroy On Load Singleton class. This class ensures continuity throughout the game.
    public static GameManager instance;
    private void Awake() {
        if (instance == null){
            instance = this;

            DontDestroyOnLoad(this.gameObject);
            
            ShuffleNames();
            InitializeCharacters();
            InitializeTileTypes();
        }
        else {
            Destroy(this);
            return;
        }
        
    }
    public static GameManager GetGameManager(){
        return instance;
    }

    public static List<Character> GetCharacters(){
        return instance.characters;
    }

    public static void LoadCampaignScene(){
        FadeIn.FadeToScene("Overworld");
    }

    public static void LoadTacticalScene(){
        FadeIn.FadeToScene("TacticalMap");
    }



    public void InitializeCharacters(){

        // Initialize list of characters.
        characters = new List<Character>();
        for (int i = 0; i < 4; i++){
            Character newChar = new Character(GetName(), Random.Range(0, 22), 6, 6);
            characters.Add(newChar);
        }

        deadCharacters = new List<Character>();
        
    }

    public void InitializeTileTypes(){
        int tileCount = 76;

        tileTypeList = new List<TileManager.TileType>();
        
        for (int i = 0; i < tileCount; i++){
            if (i == 0 || i == tileCount - 1){
                tileTypeList.Add(TileManager.TileType.Normal);
                continue;
            }

            // Randomly determine what kind of tile should be generated.
            int roll = Random.Range(0, 7);
            if (roll < 3){tileTypeList.Add(TileManager.TileType.Normal);}
            else if (roll == 3){tileTypeList.Add(TileManager.TileType.Forest);}
            else if (roll == 4){tileTypeList.Add(TileManager.TileType.Treasure);}
            else if (roll == 5){tileTypeList.Add(TileManager.TileType.Monsters);}
            else if (roll == 6){tileTypeList.Add(TileManager.TileType.Aberration);}
            else {tileTypeList.Add(TileManager.TileType.Normal);}

        }
    }

    public void ShuffleNames(){
        for (int i = 0; i < characterNamesList.Count; i++){
            int r = Random.Range(i, characterNamesList.Count);

            string temp = characterNamesList[r];
            characterNamesList[r] = characterNamesList[i];
            characterNamesList[i] = temp;
        }
    }

    public static List<TileManager.TileType> GetTileTypes(){
        return instance.tileTypeList;
    }


    // This function returns the type of tile the party is currently on.
    // This can and should be used in the Tactical Scene as well.
    public static TileManager.TileType GetCurrTileType(){
        return instance.tileTypeList[instance.currTile];
    }





    public static void AdvanceTiles(int number){
        instance.currTile += number;
    }
    public static int GetTileIndex(){
        return instance.currTile;
    }

    public static void SetPushedOn(bool pushedOn){
        instance.pushedOn = pushedOn;
    }
    public static bool GetPushedOn(){
        return instance.pushedOn;
    }


    public static void KillCharacter(Character character){
        instance.characters.Remove(character);

        instance.deadCharacters.Add(character);

        if (instance.characters.Count == 0) {
            FadeIn.FadeToScene("DefeatedScene");
        }
    }

    public static void DetermineIfWon(){
        List<Character> chars = GetCharacters();
        int sanityPar = chars.Count * 2;
        int totalSanity = 0;
        foreach(Character c in chars){
            totalSanity += c.sanity;
        }
        if (totalSanity <= sanityPar){
            instance.isGameWon = true;
        }
        else {
            instance.isGameWon = false;
        }
    }
    public static bool IsGameWon(){
        if (instance == null){return false;}
        return instance.isGameWon;
    }

    


    //
    //
    // GAME STATE
    //
    //


    // Ordered list of all characters.
    public List<Character> characters;
    public List<Character> deadCharacters;

    // Current tile index.
    public int currTile = 0;
    public bool pushedOn = false;

    // Board tile types.
    public List<TileManager.TileType> tileTypeList;

    // Name index for getting randomized names.
    public int currNameIndex = 0;



    public bool isGameWon = false;



    //
    //
    // END GAME STATE
    //
    //



    public List<string> characterNamesList;
    public string GetName(){
        string name = characterNamesList[currNameIndex];
        currNameIndex = (currNameIndex + 1) % characterNamesList.Count;
        return name;
    }
}

public class Character {

    public Character(string name, int model, int sanity, int health){
        characterName = name;
        modelIndex = model;
        this.sanity = sanity;
        this.health = health;

        isAlive = true;
    }

    public bool isAlive;

    // Name
    public string characterName;

    // Appearance / which character model this character has enabled.
    public int modelIndex;

    // 1-6 scale
    public int sanity;

    public void ChangeSanity(int difference){
        if (!isAlive){return;}

        sanity = Mathf.Min(sanity + difference, 6);
    }

    /*public void AttackOthers(){
        if (!isAlive){return;}

        GameManager.GetCharacters()[Random.Range(0,GameManager.GetCharacters().Count)].ChangeHealth(-2);
    }*/



    // 1-6 scale
    public int health;



    public void ChangeHealth(int difference){
        if (!isAlive){return;}

        health = Mathf.Clamp(health + difference, 0, 6);

        if (health <= 0){
            Die();
        }
    }

    public void Die(){
        isAlive = false;
        GameManager.KillCharacter(this);
    }


}