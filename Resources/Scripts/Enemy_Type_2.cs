using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type_2 : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        health = 40;
        damage = 10;
        coins = 25;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
