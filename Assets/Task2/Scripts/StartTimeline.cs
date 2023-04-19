using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartTimeline : MonoBehaviour
{
    private PlayableDirector _timeline;

    private void Start()
    {
        _timeline = GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _timeline.Play();
        }
    }
}
