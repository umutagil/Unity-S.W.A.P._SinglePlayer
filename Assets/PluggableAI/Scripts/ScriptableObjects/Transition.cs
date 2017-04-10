using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition
{
    public Decision decision;   
    public State trueState;     // next state if decision returns true
    public State falseState;    // next state if decision returns false
}
