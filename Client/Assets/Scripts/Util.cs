using System.Collections;
using System.Collections.Generic;
using System;

public static class Util {

	/// <summary>
	/// Gets value associated with key or returns default
	/// </summary>
	/// <param name="data">Data.</param>
	/// <param name="key">Key.</param>
	/// <param name="store">Store.</param>
	/// <param name="defaultVal">Default value.</param>
	public static T GetOrDefault<T>(Dictionary<string, object> data, string key, T defaultVal) {
		object storeObject = defaultVal;
		data.TryGetValue(key, out storeObject);
		return (T)storeObject;
	}

	public static long CurrentUnixTimestamp() {
		long unixTimestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
		return unixTimestamp;
	}
}
