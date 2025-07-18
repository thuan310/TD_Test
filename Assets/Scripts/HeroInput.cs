using UnityEngine;

public struct HeroInput {
    public Vector2 move;

    public bool skillPressed;

    public HeroInput(Vector2 move, bool skill)
    {
        this.move = move;
        this.skillPressed = skill;
    }
}