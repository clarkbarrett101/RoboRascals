using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_CompAction
{
    public void Anticipation(A_Robot robot, A_Enemy[] enemies);
    public void Action(A_Robot robot, A_Enemy[] enemies);
    public void Recovery(A_Robot robot, A_Enemy[] enemies);
}