using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type_3_Boss : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        health = 250;
        damage = 40;
        coins = 100;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
