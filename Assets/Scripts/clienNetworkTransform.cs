using Unity.Netcode.Components;
using UnityEngine;
public class clienNetworkTransform : NetworkTransform
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }

}
