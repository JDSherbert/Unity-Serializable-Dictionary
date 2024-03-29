/// <summary>
/// ©2024 JDSherbert. All rights reserved.
/// </summary>

namespace Sherbert.Framework.Generic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using UnityEngine;

    // ---------------------------------------------------------------- //
    //? Serialized Dictionary Base
    // ---------------------------------------------------------------- //

    /// <summary>
    /// Serialized Dictionary Base class. 
    /// </summary>
    public abstract class SerializableDictionary_Base
    {
        protected class Dictionary<TKey, TValue> : System.Collections.Generic.Dictionary<TKey, TValue>
        {
            public Dictionary() { }
            public Dictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary) { }
            public Dictionary(SerializationInfo serialInfo, StreamingContext streamingContext) : base(serialInfo, streamingContext) { }
        }

        public abstract class Cache { }
    }


    // ---------------------------------------------------------------- //
    //? Serialized Dictionary Base <TKey, TValue, TValueCache>
    // ---------------------------------------------------------------- //

    /// <summary>
    /// Serialized Dictionary Base class. 
    /// </summary>
    [Serializable]
    public abstract class SerializableDictionary_Base<TKey, TValue, TValueCache> : SerializableDictionary_Base, IDictionary<TKey, TValue>, IDictionary, ISerializationCallbackReceiver, IDeserializationCallback, ISerializable
    {
        Dictionary<TKey, TValue> dictionary;
        [SerializeField] TKey[] keys;
        [SerializeField] TValueCache[] values;

        // ---------------------------------------------------------------- //

        public SerializableDictionary_Base()
        {
            this.dictionary = new Dictionary<TKey, TValue>();
        }

        public SerializableDictionary_Base(IDictionary<TKey, TValue> dictionary)
        {
            this.dictionary = new Dictionary<TKey, TValue>(dictionary);
        }

        // ---------------------------------------------------------------- //

        public void OnAfterDeserialize()
        {
            if (keys != null && values != null && keys.Length == values.Length)
            {
                dictionary.Clear();
                int length = keys.Length;
                for (int i = 0; i < length; ++i)
                {
                    dictionary[keys[i]] = GetValue(values, i);
                }

                keys = null;
                values = null;
            }
        }

        public void OnBeforeSerialize()
        {
            int count = dictionary.Count;
            keys = new TKey[count];
            values = new TValueCache[count];

            int i = 0;
            foreach (var kvp in dictionary)
            {
                keys[i] = kvp.Key;
                SetValue(values, i, kvp.Value);
                ++i;
            }
        }

        // ---------------------------------------------------------------- //

        protected abstract void SetValue(TValueCache[] cache, int i, TValue value);
        protected abstract TValue GetValue(TValueCache[] cache, int i);

        // ---------------------------------------------------------------- //

        /// <summary>
        /// Replaces the values in this dictionary with values from a different dictionary. 
        /// </summary>
        /// <param name="dictionary">Dictionary reference to copy from.</param>
        public void CopyFrom(IDictionary<TKey, TValue> dictionary)
        {
            this.dictionary.Clear();
            foreach (var kvp in dictionary)
            {
                this.dictionary[kvp.Key] = kvp.Value;
            }
        }

        // ---------------------------------------------------------------- //
        #region IDictionary<TKey, TValue>
        // ---------------------------------------------------------------- //

        public ICollection<TKey> Keys { get { return ((IDictionary<TKey, TValue>)dictionary).Keys; } }
        public ICollection<TValue> Values { get { return ((IDictionary<TKey, TValue>)dictionary).Values; } }
        public int Count { get { return ((IDictionary<TKey, TValue>)dictionary).Count; } }
        public bool IsReadOnly { get { return ((IDictionary<TKey, TValue>)dictionary).IsReadOnly; } }

        public TValue this[TKey key]
        {
            get { return ((IDictionary<TKey, TValue>)dictionary)[key]; }
            set { ((IDictionary<TKey, TValue>)dictionary)[key] = value; }
        }

        // ---------------------------------------------------------------- //

        /// <summary>
        /// Adds a new Key Value Pair to this dictionary. 
        /// </summary>
        /// <param name="key">New key to add.</param>
        /// <param name="value">New value to add.</param>
        public void Add(TKey key, TValue value)
        {
            ((IDictionary<TKey, TValue>)dictionary).Add(key, value);
        }

        /// <summary>
        /// Check to see if the dictionary contains the key. 
        /// </summary>
        /// <param name="key">Key reference to check.</param>
        /// <returns> <c>true</c> if the dictionary contains the key.</returns>
        public bool ContainsKey(TKey key)
        {
            return ((IDictionary<TKey, TValue>)dictionary).ContainsKey(key);
        }

        /// <summary>
        /// Removes a Key Value Pair from the dictionary, by key reference. 
        /// </summary>
        /// <param name="key">Key reference to remove.</param>
        /// <returns> <c>true</c> if the key was removed successfully.</returns>
        public bool Remove(TKey key)
        {
            return ((IDictionary<TKey, TValue>)dictionary).Remove(key);
        }

        /// <summary>
        /// Attempts to retrieve a Value from the dictionary, by key reference. 
        /// </summary>
        /// <param name="key">Key reference.</param>
        /// <param name="value">Value reference to write out to.</param>
        /// <returns> <c>true</c> if the value was retrieved successfully.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return ((IDictionary<TKey, TValue>)dictionary).TryGetValue(key, out value);
        }

        /// <summary>
        /// Adds a new Key Value Pair to this dictionary. 
        /// </summary>
        /// <param name="item">New key value pair to add.</param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            ((IDictionary<TKey, TValue>)dictionary).Add(item);
        }

        /// <summary>
        /// Removes all Key Value Pairs in this dictionary. 
        /// </summary>
        public void Clear()
        {
            ((IDictionary<TKey, TValue>)dictionary).Clear();
        }

        /// <summary>
        /// Check to see if the dictionary contains the key value pair. 
        /// </summary>
        /// <param name="item">Key value pair reference to check.</param>
        /// <returns> <c>true</c> if the dictionary contains the key value pair.</returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<TKey, TValue>)dictionary).Contains(item);
        }

        /// <summary>
        /// Appends a range of key value pairs into an index in the dictionary. 
        /// </summary>
        /// <param name="array">Key value pair references to append.</param>
        /// <param name="arrayIndex">Array index to append at.</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((IDictionary<TKey, TValue>)dictionary).CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes a Key Value Pair from the dictionary. 
        /// </summary>
        /// <param name="item">Key value pair to remove.</param>
        /// <returns> <c>true</c> if the key value pair was removed successfully.</returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<TKey, TValue>)dictionary).Remove(item);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return ((IDictionary<TKey, TValue>)dictionary).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<TKey, TValue>)dictionary).GetEnumerator();
        }

        // ---------------------------------------------------------------- //
        #endregion IDictionary<TKey, TValue>
        // ---------------------------------------------------------------- //


        // ---------------------------------------------------------------- //
        #region IDictionary
        // ---------------------------------------------------------------- //

        public bool IsFixedSize { get { return ((IDictionary)dictionary).IsFixedSize; } }
        ICollection IDictionary.Keys { get { return ((IDictionary)dictionary).Keys; } }
        ICollection IDictionary.Values { get { return ((IDictionary)dictionary).Values; } }
        public bool IsSynchronized { get { return ((IDictionary)dictionary).IsSynchronized; } }
        public object SyncRoot { get { return ((IDictionary)dictionary).SyncRoot; } }

        public object this[object key]
        {
            get { return ((IDictionary)dictionary)[key]; }
            set { ((IDictionary)dictionary)[key] = value; }
        }

        // ---------------------------------------------------------------- //

        public void Add(object key, object value)
        {
            ((IDictionary)dictionary).Add(key, value);
        }

        public bool Contains(object key)
        {
            return ((IDictionary)dictionary).Contains(key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary)dictionary).GetEnumerator();
        }

        // ---------------------------------------------------------------- //

        /// <summary>
        /// Removes a Key Value Pair from the dictionary. 
        /// </summary>
        /// <param name="key">Key reference to remove.</param>
        /// <returns> <c>true</c> if the key value pair was removed successfully.</returns>
        public void Remove(object key)
        {
            ((IDictionary)dictionary).Remove(key);
        }

        /// <summary>
        /// Appends a range of key value pairs into an index in the dictionary. 
        /// </summary>
        /// <param name="array">Key value pair references to append.</param>
        /// <param name="index">Array index to append at.</param>
        public void CopyTo(Array array, int index)
        {
            ((IDictionary)dictionary).CopyTo(array, index);
        }

        // ---------------------------------------------------------------- //
        #endregion IDictionary
        // ---------------------------------------------------------------- //

        // ---------------------------------------------------------------- //
        #region IDeserializationCallback
        // ---------------------------------------------------------------- //

        public void OnDeserialization(object sender)
        {
            ((IDeserializationCallback)dictionary).OnDeserialization(sender);
        }

        // ---------------------------------------------------------------- //
        #endregion IDeserializationCallback
        // ---------------------------------------------------------------- //

        // ---------------------------------------------------------------- //
        #region ISerializable
        // ---------------------------------------------------------------- //

        protected SerializableDictionary_Base(SerializationInfo info, StreamingContext context)
        {
            dictionary = new Dictionary<TKey, TValue>(info, context);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)dictionary).GetObjectData(info, context);
        }

        // ---------------------------------------------------------------- //
        #endregion ISerializable
        // ---------------------------------------------------------------- //
    }

    // ---------------------------------------------------------------- //
    //? Serializable Dictionary
    // ---------------------------------------------------------------- //
    public static class SerializableDictionary
    {
        public class Cache<T> : SerializableDictionary_Base.Cache
        {
            public T data;
        }
    }

    // ---------------------------------------------------------------- //
    //? Serializable Dictionary <TKey, TValue>
    // ---------------------------------------------------------------- //

    /// <summary>
    /// Serialized Dictionary Class. Allows a dictionary-like data structure with key-value pairs that can be serialized and edited in the Unity editor. 
    /// It addresses the limitation of Unity's default Dictionary class, which cannot be directly serialized. 
    /// <see cref="ISerializationCallbackReciever"/> and <see cref="Dictionary"/>.
    ///
    /// <code>
    /// // Example usage in a MonoBehaviour script
    /// using UnityEngine;
    /// using Sherbert.Framework.Generic;
    ///
    /// public class DictionaryExample : MonoBehaviour
    /// {
    ///     [SerializeField] private SerializableDictionary<string, int> myDictionary = new();
    /// }
    /// </code>
    /// </summary>
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : SerializableDictionary_Base<TKey, TValue, TValue>
    {
        public SerializableDictionary() { }
        public SerializableDictionary(IDictionary<TKey, TValue> dict) : base(dict) { }
        protected SerializableDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }

        // ---------------------------------------------------------------- //

        protected override TValue GetValue(TValue[] values, int i)
        {
            return values[i];
        }

        protected override void SetValue(TValue[] values, int i, TValue value)
        {
            values[i] = value;
        }

        // ---------------------------------------------------------------- //
    }

    // ---------------------------------------------------------------- //
    //? Serializable Dictionary <TKey, TValue, TValueCache>
    // ---------------------------------------------------------------- //
    
    /// <summary>
    /// Serialized Dictionary Class. Allows a dictionary-like data structure with key-value pairs that can be serialized and edited in the Unity editor. 
    /// It addresses the limitation of Unity's default Dictionary class, which cannot be directly serialized. 
    /// <see cref="ISerializationCallbackReciever"/> and <see cref="Dictionary"/>.
    ///
    /// <code>
    /// // Example usage in a MonoBehaviour script
    /// using UnityEngine;
    /// using Sherbert.Framework.Generic;
    ///
    /// public class DictionaryExample : MonoBehaviour
    /// {
    ///     [SerializeField] private SerializableDictionary<string, int> myDictionary = new();
    /// }
    /// </code>
    /// </summary>
    [Serializable]
    public class SerializableDictionary<TKey, TValue, TValueCache> : SerializableDictionary_Base<TKey, TValue, TValueCache> where TValueCache : SerializableDictionary.Cache<TValue>, new()
    {
        public SerializableDictionary() { }
        public SerializableDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary) { }
        protected SerializableDictionary(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }

        // ---------------------------------------------------------------- //

        protected override TValue GetValue(TValueCache[] cache, int i)
        {
            return cache[i].data;
        }

        protected override void SetValue(TValueCache[] cache, int i, TValue value)
        {
            cache[i] = new TValueCache();
            cache[i].data = value;
        }

        // ---------------------------------------------------------------- //
    }
}
