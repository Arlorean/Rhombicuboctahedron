# Rhombicuboctahedron

[ChatGPT 4.5 Deep Research](https://openai.com/index/introducing-deep-research/) generated Rhombicuboctahedron parametric mesh for Unity.

| ![Unity Screen Recording of Rhombicuboctahedron Parametric Mesh](/docs/Rhombicuboctahedron.gif) |  ![Wikipedia Rhombicuboctahedron Animated GIF](/docs/P2-A5-P3.gif) |
|:--:|:--:|
| _[Unity Screen Recorder](https://docs.unity3d.com/Packages/com.unity.recorder@5.1/manual/index.html)_ | _[Wikipedia Animation](https://en.wikipedia.org/wiki/Rhombicuboctahedron)_ |

## Hand Written Code

I manually wroite code using the Unity ProBuilder API with the Bevel feature, which was exactly what I needed, but then I also wanted full control over the Normals and UVs, and it was never obvious to me when I should call [ProBuilderMesh.Refresh()](https://docs.unity3d.com/Packages/com.unity.probuilder@6.0/api/UnityEngine.ProBuilder.ProBuilderMesh.html#UnityEngine.ProBuilder.ProBuilderMesh.Refresh(UnityEngine.ProBuilder.RefreshMask)) and with what [RefreshMask](https://docs.unity3d.com/Packages/com.unity.probuilder@6.0/api/UnityEngine.ProBuilder.RefreshMask.html) to get it to **not** override my custom normals and UVs. This took a few days to create and debug.

| ![ProBuilder Bevel](/docs/ProBuilder%20Bevel.png) |
|:--:|
| _[ProBuilder Bevel](https://docs.unity3d.com/Packages/com.unity.probuilder@6.0/manual/Edge_Bevel.html)_ |

## AI Generated Code

Given that the shape in question was formally know as the [Rhombicuboctahedron](https://en.wikipedia.org/wiki/Rhombicuboctahedron), and is one of the [13 Archimedean Solids](https://en.wikipedia.org/wiki/Archimedean_solid), I thought this kind of problem would be a slam dunk for modern AI. So I put together this prompt and ran it through various modern AI tools:

> _Can you create me a Unity MonoBehaviour C# script that will procedurally create a Rhombicuboctahedron mesh where there is a parameter t that varies between 0 and 1 with 0 being the Rhombicuboctahedron as a perfect cube and 1 being the Rhombicuboctahedron as a perfect octahdron. I will call a ContextMenu method in the editor to call the method to generate the mesh. The game object is guaranteed to have a MeshFilter, MeshRenderer on it already and there is no need for a collider._

Here are the visual results of the various AI models in action using that prompt:
- (Claude Sonnet 3.7)[#claude-sonnet-3.7] (Anthropic)
- (Grok 3)[#grok-3] (xAI)
- (ChatGPT 4o)[#chatgpt-4o]
- (Gemini Flash 2.0)[#gemini-flash-2.0]
- (Gemini Deep Research)[#gemini-deep-research]
- (ChatGPT Deep Research 4.5)[#chatgpt-deep-research-4.5]

## Claude Sonnet 3.7

First I tried my trusted [Anthropic Claude](https://www.anthropic.com/claude) with the latest [3.7 Sonnet](https://www.anthropic.com/claude/sonnet) model and used the [AI Prompt](#ai-prompt) shown above.

| ![Claude Sonnet 3.7](/docs/Claude.png) |
|:--:|
| _[Claude Sonnet 3.7](https://www.anthropic.com/claude/sonnet)_ |

##  Grok 3

| ![Grok 3](/docs/Grok.png) |
|:--:|
| _[Grok 3](https://grok.com/)_ |

## ChatGPT 4o

| ![ChatGPT 4o](/docs/ChatGPT.png) |
|:--:|
| _[ChatGPT 4o](https://chatgpt.com/)_ |

## Gemini Flash 2.0

Google's [Gemini](https://gemini.google/) Flash 2.0 model was unable to generate working code, despite many follow up prompts to get it to fix the code.

```
IndexOutOfRangeException: Index was outside the bounds of the array.
ProceduralRhombicuboctahedron.CalculateRhombicuboctahedronVerticesAndTriangles (System.Single t, UnityEngine.Vector3[]& vertices, System.Int32[]& triangles) (at Assets/Scripts/ProceduralRhombicuboctahedron.cs:53)
ProceduralRhombicuboctahedron.GenerateMesh () (at Assets/Scripts/ProceduralRhombicuboctahedron.cs:16)
```

## Gemini Deep Research 

The results returned a while later (10s of minutes, not sure exactly how long unfortunately) and it prepared a Google Document for me detailing all the sources and it's thoughts and suggestions, together with "working code". Overall the presentation was great:

[Rhombicuboctahedron Procedural Mesh Generation](https://docs.google.com/document/d/10nyRoulTEgFuvTiwIbM9txLCUbPuh6JCgWq644082Cw/edit?usp=sharing)

The code however was unable to generate anything close to a Rhombicuboctahedron and at best it managed to generate 24 vertices, 12 inidices making 4 triangles. In other cases the results produced the same number of vertices and indices but the result degenerated to a line.

| ![Gemini Deep Research](/docs/Gemini-DeepResearch.png) |
|:--:|
| _[Gemini Deep Research](https://gemini.google/)_ |


## ChatGPT Deep Research 4.5

| ![Unity Screen Recording of Rhombicuboctahedron Parametric Mesh](/docs/Rhombicuboctahedron.gif) |  ![Wikipedia Rhombicuboctahedron Animated GIF](/docs/P2-A5-P3.gif) |
|:--:|:--:|
| _[Unity Screen Recorder](https://docs.unity3d.com/Packages/com.unity.recorder@5.1/manual/index.html)_ | _[Wikipedia Animation](https://en.wikipedia.org/wiki/Rhombicuboctahedron)_ |

[ChatGPT Deep Research Transcript](https://chatgpt.com/share/67e6f9a5-daa0-8007-9022-af811ec9d063)

