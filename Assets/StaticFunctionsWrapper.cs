using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticFunctionsWrapper : MonoBehaviour
{
    // this class only contains wrappers for static functions inside static classes
    // the reason i have to do this horrible-ness is because unity does not support
    // static functions being called from inside the editor, for things like onClick()
    // listeners on UI buttons... too bad!

    public void saveGameData()
    {
        Debug.Log("da");
        GameDataSaver.saveGameData();
    }
}
