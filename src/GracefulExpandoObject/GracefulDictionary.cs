using System.Collections.Generic;

namespace MR
{
	public class GracefulDictionary : GracefulDictionary<string, object>
	{
	}

	public class GracefulDictionary<TKey, TValue> : IDictionary<TKey, TValue>
	{
		protected Dictionary<TKey, TValue> _map = new Dictionary<TKey, TValue>();

		public TValue this[TKey key]
		{
			get
			{
				TValue val = default(TValue);
				_map.TryGetValue(key, out val);
				return val;
			}
			set
			{
				_map[key] = value;
			}
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public void Add(TKey key, TValue value)
		{
			_map.Add(key, value);
		}

		public bool ContainsKey(TKey key)
		{
			return _map.ContainsKey(key);
		}

		public ICollection<TKey> Keys
		{
			get { return _map.Keys; }
		}

		public bool Remove(TKey key)
		{
			return _map.Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return _map.TryGetValue(key, out value);
		}

		public ICollection<TValue> Values
		{
			get { return _map.Values; }
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			((IDictionary<TKey, TValue>)_map).Add(item);
		}

		public void Clear()
		{
			_map.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return ((IDictionary<TKey, TValue>)_map).Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((IDictionary<TKey, TValue>)_map).CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return _map.Count; }
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return Remove(item);
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return ((IDictionary<TKey, TValue>)_map).GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _map.GetEnumerator();
		}
	}
}
