using Unity.Netcode.Components;
using UnityEngine;
public class ClientNextworkAnimator : NetworkAnimator
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }

}
