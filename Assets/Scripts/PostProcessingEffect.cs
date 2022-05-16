using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingEffect : MonoBehaviour
{
    public float _bloom;
    public float _saturation;

    Bloom _bloobLayer = null;
    ColorGrading _colorGrading = null;

    PostProcessVolume _volume;

    private void Start()
    {
        _volume = GetComponent<PostProcessVolume>();
    }

    private void Update()
    {
        _volume.profile.TryGetSettings(out _bloobLayer);
        _volume.profile.TryGetSettings(out _colorGrading);

        _bloobLayer.enabled.value = true;
        _colorGrading.enabled.value = true;

        _bloobLayer.intensity.value = _bloom;
        _colorGrading.saturation.value = _saturation;

    }


}
