using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class SerializeDictionary<TKey, TValue> : ISerializationCallbackReceiver
{
    // ISerializationCallbackReceiver 
    //  ��ü�� Unity�� ���� ����ȭ�ǰų� ������ȭ�� �� ȣ��Ǵ� �ݹ� �޼��带 ����
    [SerializeField]
    private List<TKey> keys = new List<TKey>();
    [SerializeField]
    private List<TValue> values = new List<TValue>();

    private Dictionary<TKey, TValue> dictionary = new ();

    public TValue this[TKey key]
    {
        get => dictionary[key];
        set => dictionary[key] = value;
    }

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();

        foreach (var kvp in dictionary)
        {
            keys.Add(kvp.Key);
            values.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        dictionary = new Dictionary<TKey, TValue>();

        for (int i = 0; i != Math.Min(keys.Count, values.Count); i++)
            dictionary.Add(keys[i], values[i]);
    }

    public void Add(TKey key, TValue value)
    {
        dictionary[key] = value;
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        return dictionary.TryGetValue(key, out value);
    }

    public Dictionary<TKey, TValue> ToDictionary()
    {
        return dictionary;
    }

    public bool ContainsKey(TKey key)
        => dictionary.ContainsKey(key);
}