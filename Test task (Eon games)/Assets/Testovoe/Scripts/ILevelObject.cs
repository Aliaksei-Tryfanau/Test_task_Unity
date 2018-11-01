using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelObject
{
    void Success();
    void Lose();
    void CheckActivation();
}
