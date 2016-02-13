using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace MR
{
	public class GracefulExpandoObject : DynamicObject, IDictionary<string, object>
	{
		protected GracefulDictionary _map = new GracefulDictionary();

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

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			result = _map[binder.Name];
			return true;
		}

		public bool TryGetValue(string key, out object value)
		{
			return ((IDictionary<string, object>)_map).TryGetValue(key, out value);
		}

		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			_map[binder.Name] = value;
			return true;
		}

		public static GracefulExpandoObject FromObject(object obj, bool deep = false)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(nameof(obj));
			}

			return deep ? FromObjectDeep(obj) : FromObjectShallow(obj);
		}

		private static GracefulExpandoObject FromObjectShallow(object obj)
		{
			var geo = new GracefulExpandoObject();
			var type = obj.GetType();
			var properties = GetProperties(type);

			foreach (var property in properties)
			{
				var value = property.GetValue(obj);
				if (!IsPrimitive(property.PropertyType) && value == null)
				{
					continue;
				}
				geo.Add(property.Name, value);
			}

			return geo;
		}

		private static GracefulExpandoObject FromObjectDeep(object obj)
		{
			var geo = new GracefulExpandoObject();
			var type = obj.GetType();
			var properties = GetProperties(type);

			foreach (var property in properties)
			{
				if (IsPrimitive(property.PropertyType))
				{
					var value = property.GetValue(obj);
					geo.Add(property.Name, value);
				}
				else
				{
					var value = property.GetValue(obj);
					if (value != null)
					{
						geo.Add(property.Name, FromObjectDeep(value));
					}
				}
			}

			return geo;
		}

		private static IEnumerable<PropertyInfo> GetProperties(Type type) =>
			type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

		private static bool IsPrimitive(Type type) =>
			type.GetTypeInfo().IsPrimitive ||
			type.GetTypeInfo().IsValueType ||
			type == typeof(string);
	}
}
