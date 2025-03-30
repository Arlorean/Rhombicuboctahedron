# Rhombicuboctahedron

[ChatGPT 4.5 Deep Research](https://openai.com/index/introducing-deep-research/) generated Rhombicuboctahedron parametric mesh for Unity.

| ![Unity Screen Recording of Rhombicuboctahedron Parametric Mesh](/docs/Rhombicuboctahedron.gif) |  ![Wikipedia Rhombicuboctahedron Animated GIF](/docs/P2-A5-P3.gif) |
|:--:|:--:|
| _[Unity Screen Recorder](https://docs.unity3d.com/Packages/com.unity.recorder@5.1/manual/index.html)_ | _[Wikipedia Animation](https://en.wikipedia.org/wiki/Rhombicuboctahedron)_ |

## ProBuilder Bevel

I wanted to generate voxel grid for a Unity game level with a parametric bevel. I started using the Unity ProBuilder API with the Bevel feature, which was exactly what I needed, but then I also wanted full control over the Normals and UVs and it was never obvious to me when I should call [ProBuilderMesh.Refresh()](https://docs.unity3d.com/Packages/com.unity.probuilder@6.0/api/UnityEngine.ProBuilder.ProBuilderMesh.html#UnityEngine.ProBuilder.ProBuilderMesh.Refresh(UnityEngine.ProBuilder.RefreshMask)) and with what [RefreshMask](https://docs.unity3d.com/Packages/com.unity.probuilder@6.0/api/UnityEngine.ProBuilder.RefreshMask.html) to get it to **not** override my custom normals and UVs.

| ![ProBuilder Bevel](/docs/ProBuilder Bevel.png) |
|:--:|
| _[ProBuilder Bevel](https://docs.unity3d.com/Packages/com.unity.probuilder@6.0/manual/Edge_Bevel.html)_ |

I thought this kind of problem would be a slam dunk for modern AI...

