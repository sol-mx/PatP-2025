using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventSequence", menuName = "EventSequence", order = 6)]
public class EventSequence : ScriptableObject
{
    [field: SerializeField] public List<EventProfile> Val { get; private set; }
}
