using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audiosource;
    // then elsewhere when you want to invoke it:

    void Start()
    {
        audiosource.PlayDelayed(0.79f);
    }

}
