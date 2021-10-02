using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TileSystem;

public class GameManager : MonoBehaviour
{
    public TileNavigator dias;
    public DiceRoll diceRoll;

    public void NextTurn() {
        int numberOfStepsForDiasToMoveForward = diceRoll.RollDice();
        dias.MoveForward(numberOfStepsForDiasToMoveForward);
    }
}
