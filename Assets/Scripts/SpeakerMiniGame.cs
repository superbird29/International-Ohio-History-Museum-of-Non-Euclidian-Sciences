using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerMiniGame : MonoBehaviour
{
    public List<GameObject> Speakers;
    /// <summary>
    /// Loops through the speakers to activate them. By trigguring the activate function on them
    /// </summary>
    public void ActiveSpeakerMiniGame()
    {
        GameManager.Instance._eggManager.MouseOverLAyOn = false;
        for(int i = 0; i < Speakers.Count; i++)
        {
            Speakers[i].GetComponent<SpeakerSlider>().Activate();
        }
    }
}
