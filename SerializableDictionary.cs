// ©2023 JDSherbert

using UnityEngine;
using System;
using System.Collections.Generic;

namespace Sherbert.Framework.Generic
{
    /// <summary>
    /// Serialized Dictionary Class. Allows a dictionary-like data structure with key-value pairs that can be serialized and edited in the Unity editor. 
    /// It addresses the limitation of Unity's default Dictionary class, which cannot be directly serialized. 
    /// <see cref="ISerializationCallbackReciever"/> and 
    /// <see cref="Dictionary"/>.
    ///
    /// Note that:
    /// <code>
    /// // Example usage in a MonoBehaviour script
    /// public class DictionaryExample : MonoBehaviour
    /// {
    ///     [SerializeField] private SerializableDictionary<string, int> myDictionary = new();
    /// }
    /// </code>
    /// </summary>
    [System.Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<TKey> keys = new List<TKey>();
        [SerializeField] private List<TValue> values = new List<TValue>();

        // Unity serialization callback before serialization
        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();

            foreach (var kvp in this)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }

        // Unity serialization callback after deserialization
        public void OnAfterDeserialize()
        {
            this.Clear();

            if (keys.Count != values.Count) throw new Exception("The number of keys and values in the dictionary does not match.");
        
            for (int i = 0; i < keys.Count; i++) this[keys[i]] = values[i];
        }
    }
}
