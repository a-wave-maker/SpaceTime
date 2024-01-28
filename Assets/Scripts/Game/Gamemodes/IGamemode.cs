using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGamemode
{
    public bool WinConditionMet();
    public bool LossConditionMet();

}
