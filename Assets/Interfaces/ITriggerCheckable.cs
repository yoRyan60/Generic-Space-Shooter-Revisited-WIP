using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCheckable
{
    bool IsInAttackRange {  get; set; }

    void SetInAttackRange(bool isInAttackRange);
}
