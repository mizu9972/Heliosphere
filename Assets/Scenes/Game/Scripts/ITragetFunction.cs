using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITragetFunction
{
    void Hit();
}


public interface ITargetFunctionByTransform
{
    void Hit(Transform Trans);
}
