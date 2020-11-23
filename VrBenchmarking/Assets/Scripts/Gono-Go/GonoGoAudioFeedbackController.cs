using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonoGoAudioFeedbackController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip audio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stick"))
        {
            AudioSource.PlayClipAtPoint(audio
                , transform.position, 1.0f);
        }
    }
}
