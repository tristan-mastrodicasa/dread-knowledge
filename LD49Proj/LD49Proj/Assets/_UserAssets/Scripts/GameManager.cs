using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Don't Destroy On Load Singleton class. This class ensures continuity throughout the game.
    private static GameManager instance;
    private void Awake() {
        if (instance == null){
            instance = this;

            DontDestroyOnLoad(this.gameObject);

            InitializeCharacters();
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
        SceneManager.LoadScene("Overworld");
    }

    public static void LoadTacticalScene(){
        SceneManager.LoadScene("TacticalMap");
    }



    public void InitializeCharacters(){

        // Initialize list of characters.
        characters = new List<Character>();
        for (int i = 0; i < 4; i++){
            Character newChar = new Character(GetName(), Random.Range(0, 22), 6, 6);
            characters.Add(newChar);
        }
        
    }


    public static void AdvanceTiles(int number){
        instance.currTile += number;
    }
    public static int GetTileIndex(){
        return instance.currTile;
    }



    //
    // GAME STATE
    //


    // Ordered list of all characters.
    public List<Character> characters;


    // Current tile index.
    public int currTile = 0;

    

    // Name index for getting randomized names.
    public int currNameIndex = 0;



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
    }

    // Name
    public string characterName;

    // Appearance / which character model this character has enabled.
    public int modelIndex;

    // 1-6 scale
    public int sanity;

    // 1-6 scale
    public int health;


}