﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Monster_AI_Anim : MonoBehaviour {
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




    Animator animH0R;
    Monster_AI getAi;
    float speedchange;
    int targetCurrentState;
    float speedChAnimator;



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void Awake()
    {
        //=======================
        
        animH0R = GetComponent<Animator>();
        getAi = GetComponent<Monster_AI>();

        //=======================
    }



    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        

    void LateUpdate()
    {
        //=======================

        speedchange = getAi.speedchange;
        targetCurrentState = getAi.targetCurrentState;

        //=======================


        if (animH0R.GetInteger("State") != targetCurrentState) animH0R.SetInteger("State", targetCurrentState);

        //----



        if (targetCurrentState == 0 && speedchange == 0.8f)
        {
            if (animH0R.GetInteger("Walk") != 1) animH0R.SetInteger("Walk", 1);
        }
        else
        {
            if (animH0R.GetInteger("Walk") != 0) animH0R.SetInteger("Walk", 0);
        }


        //=======================
    }









    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
