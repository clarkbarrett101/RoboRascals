using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class I_CompAction: ScriptableObject
{
    public abstract void Anticipation(A_Robot robot);
    public abstract void Action(A_Robot robot);
    public abstract void Recovery(A_Robot robot);
    public abstract void OnHit(I_Actor target);
}