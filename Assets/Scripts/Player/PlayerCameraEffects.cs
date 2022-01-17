using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerCameraEffects : MonoBehaviour {

    [SerializeField] private Camera playerCam;
    
    private LensDistortion lens;
    public float distortionEffectTime;
    private bool isDistorted;

    void Awake() {
        lens = (LensDistortion) playerCam.GetComponent<Volume>().profile.components[0];
    }

    public void AddDistortion(float time) {
        distortionEffectTime += time;
        if (!isDistorted) StartCoroutine(DistortionEffect());
    }
    
    private IEnumerator DistortionEffect() {
        isDistorted = true;
        float xModifier = 0.02f;
        float yModifier = 0.02f;

        while (distortionEffectTime > 0) {
            lens.xMultiplier.value += xModifier;
            lens.yMultiplier.value += xModifier;
            if (lens.xMultiplier.value >= 1 || lens.xMultiplier.value <= 0) xModifier = -xModifier;
            if (lens.yMultiplier.value >= 1 || lens.yMultiplier.value <= 0) yModifier = -yModifier;
            distortionEffectTime -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        isDistorted = false;
        distortionEffectTime = 0;
        lens.xMultiplier.value = 0;
        lens.yMultiplier.value = 0;
    }
}
