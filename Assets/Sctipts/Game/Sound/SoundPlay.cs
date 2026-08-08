using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    public void PlayDecide()
    {
        SoundMgr.instance.Play(SEkey.GetL);
    }
}
