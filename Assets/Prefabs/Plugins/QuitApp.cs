using System.Runtime.InteropServices;
using UnityEngine;

public class QuitApp : MonoBehaviour
{
    [DllImport("__Internal")]
    public static extern void closeTab();
}
