using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RoleManager : MonoBehaviour
{
    public static RoleManager instance;
    private void Awake() {
        instance = this;
    }



    public enum Roles {Sleep, Guard, Heal, Meditate, Loot}

    public List<DraggableName> draggableNames;
    public List<RoleSlot> roleSlots;
    public List<TacSpotManager> spots;


    public Image attackImage;
    public Text attackText;

    public AnimationCurve attackAnimCurve;


    public void PopulateSpots(){
        for(int i = 0; i < 6; i++){
            
            if ( i >= GameManager.GetCharacters().Count ){
                spots[i].ShowCharacter(null);
                continue;
            }

            spots[i].ShowCharacter(GameManager.GetCharacters()[i]);
            
        }
    }

    public void SetUpNameAndSleepSlots(){
        for(int i = 0; i < 6; i++){

            if ( i >= GameManager.GetCharacters().Count ){

                draggableNames[i].gameObject.SetActive(false);

                roleSlots[i].gameObject.SetActive(false);
                continue;
            }

            draggableNames[i].GetComponentInChildren<Text>().text = GameManager.GetCharacters()[i].characterName;
            draggableNames[i].character = GameManager.GetCharacters()[i];

            roleSlots[i].character = GameManager.GetCharacters()[i];

        }
    }

    public void DisableNameDragging(){
        foreach(DraggableName d in draggableNames){
            if (!d.gameObject.activeInHierarchy){continue;}

            d.enabled = false;
        }
    }


    private void Start(){
        PopulateSpots();
        SetUpNameAndSleepSlots();
        StartCoroutine(PlayOutTileType());
    }

    


    public void ReturnToCampaign(){
        GameManager.LoadCampaignScene();
    }

    public void ExecuteDecisions(){
        StartCoroutine(PlayOutDecisions());
    }


    public IEnumerator PlayOutTileType(){
        if (GameManager.GetPushedOn()){
            yield return new WaitForSeconds(2f);
            Popup.DisplayPopup("Pushing on takes its toll...");

            foreach(DraggableName d in draggableNames){
                if (!d.gameObject.activeInHierarchy){continue;}

                int damage = Random.Range(0, 2);
                ChangeHealth(d.character, -damage);
                if (damage != 0){yield return new WaitForSeconds(2f);}

                int sanityDamage = Random.Range(0, 2);
                if (sanityDamage == 0){continue;}
                ChangeSanity(d.character, -sanityDamage);
                yield return new WaitForSeconds(2f);
                if (d.character.sanity < 1){
                    StartCoroutine(HandleInsanity(d.character));
                    while (d.character.sanity < 1){yield return null;}
                }
            }
        }
        

        

        TileManager.TileType tileType = GameManager.GetCurrTileType();

        switch(tileType){
            case TileManager.TileType.Normal:
                break;

            case TileManager.TileType.Treasure:
                break;
            
            case TileManager.TileType.Forest:
                break;

            case TileManager.TileType.Monsters:
                Popup.DisplayPopup("The beasts descend...");
                yield return new WaitForSeconds(2f);

                foreach(DraggableName d in draggableNames){
                    if (!d.gameObject.activeInHierarchy){continue;}

                    int damage = Random.Range(0, 2);
                    ChangeHealth(d.character, -damage);
                    if (damage != 0){yield return new WaitForSeconds(2f);}
                }
                break;
            
            case TileManager.TileType.Aberration:
                Popup.DisplayPopup("The horror sinks in...");
                yield return new WaitForSeconds(2f);

                foreach(DraggableName d in draggableNames){
                    if (!d.gameObject.activeInHierarchy){continue;}

                    int sanityDamage = Random.Range(1, 3);
                    ChangeSanity(d.character, -sanityDamage);
                    yield return new WaitForSeconds(2f);
                    if (d.character.sanity < 1){
                        StartCoroutine(HandleInsanity(d.character));
                        while (d.character.sanity < 1){yield return null;}
                    }

                }
                break;
            
            default:
                break;
        }

        onTileResultsFinished.Invoke();
    }

    public UnityEvent onTileResultsFinished;


    public IEnumerator PlayOutDecisions(){
        
        bool isAttacked = GetIsAttacked();
        bool isGuarded = false;
        Character guard = null;

        yield return new WaitForSeconds(1f);

        // ROLE PHASE
        foreach(DraggableName d in draggableNames){
            if (!d.gameObject.activeInHierarchy){continue;}

            switch(d.currentSlot.thisRole){
                case Roles.Guard:
                    isGuarded = true;
                    guard = d.character;

                    ChangeSanity(d.character, -1);
                    yield return new WaitForSeconds(2f);
                    if (d.character.sanity < 1){
                        StartCoroutine(HandleInsanity(d.character));
                        while (d.character.sanity < 1){yield return null;}
                    }

                    break;

                case Roles.Heal:
                    int healAmount = Random.Range(1, 3);
                    ChangeHealth(d.character, healAmount);
                    yield return new WaitForSeconds(2f);

                    ChangeSanity(d.character, -1);
                    yield return new WaitForSeconds(2f);
                    if (d.character.sanity < 1){
                        StartCoroutine(HandleInsanity(d.character));
                        while (d.character.sanity < 1){yield return null;}
                    }

                    break;
                
                case Roles.Loot:
                    int heal = Random.Range(1, 5);
                    ChangeHealth(d.character, heal);
                    yield return new WaitForSeconds(2f);

                    int sanityHeal = Random.Range(1, 5);
                    ChangeSanity(d.character, sanityHeal);
                    yield return new WaitForSeconds(2f);

                    break;

                case Roles.Meditate:
                    int sanityHealAmount = Random.Range(1, 3);
                    ChangeSanity(d.character, sanityHealAmount);
                    yield return new WaitForSeconds(2f);
                    break;

                default:
                    break;
            }
            
        }

        // ATTACK ANNOUNCEMENT PHASE
        float attackElapsed = 0f;
        attackText.text = isAttacked ? "Predators Lurk..." : "A Quiet Night...";
        while (attackElapsed < 4f){
            attackImage.color = new Color(attackImage.color.r, attackImage.color.g, attackImage.color.b, attackAnimCurve.Evaluate(attackElapsed/4f));
            attackText.color  = new Color(attackText.color.r, attackText.color.g, attackText.color.b, attackAnimCurve.Evaluate(attackElapsed/4f));
            
            attackElapsed += Time.deltaTime;
            yield return null;
        }
        attackImage.color = new Color(attackImage.color.r, attackImage.color.g, attackImage.color.b, 0f);
        attackText.color  = new Color(attackText.color.r, attackText.color.g, attackText.color.b, 0f);


        // ATTACK PHASE
        if (isAttacked){
            if (isGuarded){
                int damage = Random.Range(1, 4);
                ChangeHealth(guard, -damage);
                yield return new WaitForSeconds(2f);
            }
            else {
                foreach(DraggableName d in draggableNames){
                    if (!d.gameObject.activeInHierarchy){continue;}

                    int damage = Random.Range(1, 3);
                    ChangeHealth(d.character, -damage);
                    yield return new WaitForSeconds(2f);
                }
            }
        }



        onDecisionsFinished.Invoke();
    }

    public UnityEvent onDecisionsFinished;


    public IEnumerator HandleInsanity(Character character){
        while (character.sanity < 1){
            PopupInsanity(character);
            yield return new WaitForSeconds(2f);

            Character target = GameManager.GetCharacters()[Random.Range(0, GameManager.GetCharacters().Count)];
            int damage = Random.Range(1, 3);
            ChangeHealth(target, -damage);
            yield return new WaitForSeconds(2f);

            character.sanity++;
        }
    }

    public void PopupInsanity(Character character){
        Popup.DisplayPopup(character.characterName + " lashes out...");
    }


    private bool GetIsGuarded(){
        foreach(DraggableName d in draggableNames){
            if (!d.gameObject.activeInHierarchy){continue;}

            if (d.currentSlot.thisRole == Roles.Guard){
                return true;
            }
        }

        return false;
    }

    private bool GetIsAttacked(){
        // Roll is 0-2
        int roll = Random.Range(0, 3);

        TileManager.TileType tile = GameManager.GetCurrTileType();
        switch (tile){
            case TileManager.TileType.Forest:
                return roll <= 1;
            
            case TileManager.TileType.Monsters:
                return true;
            
            default:
                return roll == 0;

        }

    }







    private void ChangeSanity(Character c, int difference){
        c.ChangeSanity(difference);
        if (difference < 0){
            GetSpot(c).charIndicator.IndicateInsanity();
        }
        if (difference > 0){
            GetSpot(c).charIndicator.IndicateSanity();
        }
        
    }
    private void ChangeHealth(Character c, int difference){
        c.ChangeHealth(difference);

        if (!c.isAlive){
            GetSpot(c).charIndicator.IndicateDeath();
            return;
        }

        if (difference < 0){
            if (GetSpot(c) == null){return;}
            GetSpot(c).charIndicator.IndicateDamage();
        }
        if (difference > 0){
            if (GetSpot(c) == null){return;}
            GetSpot(c).charIndicator.IndicateHeal();
        }
    }





    private TacSpotManager GetSpot(Character character){

        for(int i = 0; i < draggableNames.Count; i++){
            if (draggableNames[i].character == character){
                return spots[i];
            }
        }
        return null;
    }
    

    
}
