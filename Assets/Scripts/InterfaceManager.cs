using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public AudioClip Click;
    private AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        _source.PlayOneShot(Click);
    }
}
