using UnityEngine;

public class Enemy_Temp : Enemy
{
    protected override void Awake() {
        base.Awake(); 
        health = 3;
    }
    protected override void Attack() {
        //throw new System.NotImplementedException();
    }

    protected override void Move() {
       // throw new System.NotImplementedException();
    }

}
