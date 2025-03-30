using UnityEngine;
using System.Collections;

public class FrameGenerator : MonoBehaviour {
    public RhombicuboctahedronGenerator generator;
    public int totalFrames = 64;

    IEnumerator Start() {
        yield return new WaitForEndOfFrame();

        float step = 1f / (totalFrames);
        for (int i = 0; i < totalFrames; i++) {
            yield return new WaitForEndOfFrame();

            var t = (float)i / totalFrames;
            generator.transform.rotation = Quaternion.Euler(0f, t * 180, 0f);
            generator.t = Mathf.Abs(Mathf.Lerp(-1f, 1f, t));
        }
    }
}
