using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public interface TimedInputHandler : IEventSystemHandler {

    void HandleTimedInput();
}
