# Rhombicuboctahedron

[ChatGPT 4.5 Deep Research](https://openai.com/index/introducing-deep-research/) generated Unity C# code to create a parametric [Rhombicuboctahedron](https://en.wikipedia.org/wiki/Rhombicuboctahedron) mesh.

| ![Unity Screen Recording of Rhombicuboctahedron Parametric Mesh](/docs/Rhombicuboctahedron.gif) |  ![Wikipedia Rhombicuboctahedron Animated GIF](/docs/P2-A5-P3.gif) |
|:--:|:--:|
| _[Unity Animation](https://docs.unity3d.com/Packages/com.unity.recorder@5.1/manual/index.html)_ | _[Wikipedia Animation](https://en.wikipedia.org/wiki/Rhombicuboctahedron)_ |

## AI Generated Code

The [Rhombicuboctahedron](https://en.wikipedia.org/wiki/Rhombicuboctahedron) is well studied and is one of the [13 Archimedean Solids](https://en.wikipedia.org/wiki/Archimedean_solid).

Given that, I thought this would be a slam dunk for modern AI, so I tested various AI models using this prompt:
> _Can you create me a Unity MonoBehaviour C# script that will procedurally create a Rhombicuboctahedron mesh where there is a parameter t that varies between 0 and 1 with 0 being the Rhombicuboctahedron as a perfect cube and 1 being the Rhombicuboctahedron as a perfect octahdron. I will call a ContextMenu method in the editor to call the method to generate the mesh. The game object is guaranteed to have a MeshFilter, MeshRenderer on it already and there is no need for a collider._

### Results

| ![](/docs/Claude.png) | ![](/docs/Grok.png) | ![](/docs/ChatGPT.png) |
|:-:|:--:|:--:|
| _[Claude Sonnet 3.7](https://www.anthropic.com/claude/sonnet)_ | _[Grok 3](https://grok.com/)_ | _[ChatGPT 4o](https://chatgpt.com/)_ |
| ![](/docs/Gemini.png) | ![](/docs/Gemini-DeepResearch.png) | ![](/docs/Rhombicuboctahedron-320x320.gif) |
| _[Gemini Flash 2.0](https://gemini.google/)_ | _[Gemini Deep Research](https://gemini.google/)_ | _[ChatGPT Deep Research](https://openai.com/index/introducing-gpt-4-5/)_ |

### Summary

| Provider | Model | Result |
|:--:|--|--|
| Anthropic | [Claude Sonnet 3.7](https://www.anthropic.com/claude/sonnet) | 18 prompt revisions were unable to results, face winding order was the main problem |
| xAI | [Grok 3](https://grok.com/) | Good first attempt but prompt revisions didn't help |
| OpenAI | [ChatGPT 4o](https://chatgpt.com/) | Poor start and prompt revisions didn't improve things |
| Google | [Gemini Flash 2.0](https://gemini.google/) | Gemini was unable to generate any compilable code despite prompt revisions |
| Google | [Gemini Deep Research](https://gemini.google/) | Look promising but the results were worse than others |
| OpenAI | [ChatGPT Deep Research 4.5](https://openai.com/index/introducing-gpt-4-5/) | Almost perfect, one compile error on first try, single prompt revision and it just worked! |

### Gemini Flash 2.0

Google's [Gemini](https://gemini.google/) Flash 2.0 model was unable to generate working code, despite many follow up prompts to get it to fix the code.

```
IndexOutOfRangeException: Index was outside the bounds of the array.
ProceduralRhombicuboctahedron.CalculateRhombicuboctahedronVerticesAndTriangles (System.Single t, UnityEngine.Vector3[]& vertices, System.Int32[]& triangles) (at Assets/Scripts/ProceduralRhombicuboctahedron.cs:53)
ProceduralRhombicuboctahedron.GenerateMesh () (at Assets/Scripts/ProceduralRhombicuboctahedron.cs:16)
```

### Gemini Deep Research 

The results returned a while later (10s of minutes, not sure exactly how long unfortunately) and it prepared a Google Document for me detailing all the sources and it's thoughts and suggestions, together with "working code". Overall the presentation was great:

[Gemini Deep Research Transcript](https://docs.google.com/document/d/10nyRoulTEgFuvTiwIbM9txLCUbPuh6JCgWq644082Cw/edit?usp=sharing)

The code however was unable to generate anything close to a Rhombicuboctahedron and at best it managed to generate 24 vertices, 12 inidices making 4 triangles. In other cases the results produced the same number of vertices and indices but the result degenerated to a line.

### ChatGPT Deep Research 4.5

Oustanding results after 26 minutes of research and a single follow up prompt to fix a small compile error. All values of 't' from 0.0 (Cube) to 0.5 (Rhombicuboctahedron) to 1.0 (Regular Octahedron) produced the expected results with perfect face vertex winding order, vertex normals and with another small prompt it successfully added verteex colors as shown in the Unity screen recording GIF. It also generated me a Unity 6.0 URP Lit shader in order to render the vertex colors that were written into the mesh.

> [ChatGPT Deep Research Transcript](https://chatgpt.com/share/67e6f9a5-daa0-8007-9022-af811ec9d063)

| ![](/docs/ChatGPT-DeepResearch-t=0.png) | ![](/docs/ChatGPT-DeepResearch-t=0.5.png) | ![](/docs/ChatGPT-DeepResearch-t=1.png) |
|:--:|:--:|:--:|
| _t=0.0_ | _t=0.5_ | _t=1.0_ |


## Hand Written Code

I manually wrote code using the Unity ProBuilder API with the Bevel feature, which was exactly what I needed, but then I also wanted full control over the Normals and UVs, and it was never obvious to me when I should call [ProBuilderMesh.Refresh()](https://docs.unity3d.com/Packages/com.unity.probuilder@6.0/api/UnityEngine.ProBuilder.ProBuilderMesh.html#UnityEngine.ProBuilder.ProBuilderMesh.Refresh(UnityEngine.ProBuilder.RefreshMask)) and with what [RefreshMask](https://docs.unity3d.com/Packages/com.unity.probuilder@6.0/api/UnityEngine.ProBuilder.RefreshMask.html) to get it to **not** override my custom normals and UVs. This took a few days to create and debug.

| ![](/docs/ProBuilder%20Bevel.png) |
|:--:|
| _[ProBuilder Bevel](https://docs.unity3d.com/Packages/com.unity.probuilder@6.0/manual/Edge_Bevel.html)_ |


## Notes

The tests were performed during 19th-28th March 2025 using a paid subscription to Claude and ChatGPT, but not for Grok or Gemini.

The [Rhombicuboctahedron.gif](/docs/Rhombicuboctahedron.gif) animated GIF file was created using the [Unity Screen Recorder](https://docs.unity3d.com/Packages/com.unity.recorder@5.1/manual/index.html).
| ![Unity Screen Recorder GIF](/docs/Rhombicuboctahedron-GitHubPreview.gif) |
|:--:|
| _[Unity Screen Recorder GIF](https://docs.unity3d.com/Packages/com.unity.recorder@5.1/manual/index.html)_ |

![Unity Screen Recorder](/docs/UnityScreenRecorder.png)

[Failed](/docs/Gemini.png) PNG image from [pngtree.com](https://pngtree.com/freepng/failed-icon_6612292.html)
