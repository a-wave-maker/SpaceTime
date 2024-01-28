using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGamemode : MonoBehaviour, IGamemode
{
    public abstract bool WinConditionMet();
    public abstract bool LossConditionMet();

    public abstract void ResetMode();
}
