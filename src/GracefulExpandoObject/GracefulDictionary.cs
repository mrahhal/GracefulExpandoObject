using System.Collections.Generic;

namespace MR
{
	public class GracefulDictionary : IDictionary<string, object>
	{
		protected Dictionary<string, object> _map = new Dictionary<string, object>();

		public object this[string key]
		{
			get
			{
				object val = null;
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

		public void Add(string key, object value)
		{
			_map.Add(key, value);
		}

		public bool ContainsKey(string key)
		{
			return _map.ContainsKey(key);
		}

		public ICollection<string> Keys
		{
			get { return _map.Keys; }
		}

		public bool Remove(string key)
		{
			return _map.Remove(key);
		}

		public bool TryGetValue(string key, out object value)
		{
			return _map.TryGetValue(key, out value);
		}

		public ICollection<object> Values
		{
			get { return _map.Values; }
		}

		public void Add(KeyValuePair<string, object> item)
		{
			((IDictionary<string, object>)_map).Add(item);
		}

		public void Clear()
		{
			_map.Clear();
		}

		public bool Contains(KeyValuePair<string, object> item)
		{
			return ((IDictionary<string, object>)_map).Contains(item);
		}

		public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			((IDictionary<string, object>)_map).CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return _map.Count; }
		}

		public bool Remove(KeyValuePair<string, object> item)
		{
			return Remove(item);
		}

		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return ((IDictionary<string, object>)_map).GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _map.GetEnumerator();
		}
	}
}
