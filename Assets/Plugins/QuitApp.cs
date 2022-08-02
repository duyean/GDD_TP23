using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class QuitApp : MonoBehaviour
{
    [DllImport("__Internal")]
    public static extern void closeTab();
}
