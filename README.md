![image](https://github.com/JDSherbert/Unity-Serializable-Dictionary/assets/43964243/3e2ca8b0-06ae-4d1b-91c2-d104916eee5a)

# Unity-Serializable-Dictionary

<!-- Header Start -->
  <a href = "https://docs.unity.com/"> <img align="left" img height="40" img width="40" src="https://cdn.simpleicons.org/unity/white"> </a> 
  <a href = "https://learn.microsoft.com/en-us/dotnet/csharp"> <img align="left" img height="40" img width="40" src="https://cdn.simpleicons.org/csharp"> </a>
<img align="right" alt="Stars Badge" src="https://img.shields.io/github/stars/jdsherbert/Unity-Serializable-Dictionary?label=%E2%AD%90"/>
<img align="right" alt="Forks Badge" src="https://img.shields.io/github/forks/jdsherbert/Unity-Serializable-Dictionary?label=%F0%9F%8D%B4"/>
<img align="right" alt="Watchers Badge" src="https://img.shields.io/github/watchers/jdsherbert/Unity-Serializable-Dictionary?label=%F0%9F%91%81%EF%B8%8F"/>
<img align="right" alt="Issues Badge" src="https://img.shields.io/github/issues/jdsherbert/Unity-Serializable-Dictionary?label=%E2%9A%A0%EF%B8%8F"/>
<img align="right" src="https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https%3A%2F%2Fgithub.com%2FJDSherbert%2FUnity-Serializable-Dictionary%2Fhit-counter%2FREADME&count_bg=%2379C83D&title_bg=%23555555&labelColor=0E1128&title=🔍&style=for-the-badge">
  <br></br>
  -----------------------------------------------------------------------
  <a href="https://unity.com/"> 
  <img align="top" alt="Extension Tool For Unity" src="https://img.shields.io/badge/Extension%20Tool%20For%20Unity-FFFFFF?style=for-the-badge&logo=unity&logoColor=black&color=black&labelColor=FFFFFF"> </a>

  <a href="https://choosealicense.com/licenses/mit/"> 
  <img align="right" alt="License" src="https://img.shields.io/badge/License%20:%20MIT-black?style=for-the-badge&logo=mit&logoColor=white&color=black&labelColor=black"> </a>
  
  -----------------------------------------------------------------------
Dictionaries cannot be serialized and displayed in the Unity inspector as is. 
This pisses me off greatly, so I decided to write a quick and dirty code class to allow Dictionary Serialization in Unity.

Add this script to your project and discover the power of Dictionary Serialization.

Features:
- Allows Dictionary display in the inspector
- Allows values to be serialized.

Usage:
1. Simply add this script to your project.
2. Example Usage:

```cs
using UnityEngine;
using Sherbert.Framework.Generic;

public class DictionaryExample : MonoBehaviour
{
    [SerializeField] private SerializableDictionary<string, int> myDictionary = new();
}
```

 -----------------------------------------------------------------------
## Overview

This Serializable Dictionary class is a custom implementation that allows you to have a dictionary-like data structure with key-value pairs that can be serialized and edited in the Unity editor. It addresses the limitation of Unity's default Dictionary class, which cannot be directly serialized. To create a Serializable Dictionary, you typically create a custom class derived from MonoBehaviour or ScriptableObject and define a dictionary field inside it. You then apply serialization attributes to the dictionary field to make it visible and editable in the Unity inspector.

To achieve serialization, the Serializable Dictionary class usually implements the ISerializationCallbackReceiver interface. This interface provides two callback methods: OnBeforeSerialize() and OnAfterDeserialize(). During serialization, the dictionary is converted into two separate lists: one for keys and another for values. These lists are then serialized. After deserialization, the lists are rebuilt into the dictionary structure.

By using the Serializable Dictionary class, you can easily create and manage key-value pairs directly in the Unity editor, allowing for dynamic customization and modification of data. This is particularly useful for scenarios where you need to store and modify structured data that should persist between editor sessions or during gameplay.

Note that while the Serializable Dictionary class provides a convenient way to work with serialized dictionaries in Unity, it is not a built-in Unity feature.
