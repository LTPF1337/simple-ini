public class INI
{
	private string fileName = "", fileText = "";
	private bool isJsonValid = false;

	public INI(string fileName)
	{
		this.fileName = fileName;

		if (!File.Exists(fileName))
		{
			File.WriteAllText(fileName, "");
			return;
		}
	}

	public void readSettings()
	{
		fileText = File.ReadAllText(fileName);
		isJsonValid = Deps.IsJsonValid(fileText);
	}

	public object readItem(string item)
	{
		if (!isJsonValid)
			return null;

		dynamic json = JsonConvert.DeserializeObject(fileText);
		var retValue = json[item];

		if (!fileText.Contains(item))
			return null;

		return retValue;
	}

	public bool readBool(string item, bool normal = false)
	{
		var value = readItem(item);

		if (value == null)
		{
			writeItem(item, normal);
			return normal;
		}

		return Convert.ToBoolean(value);
	}

	public long readLong(string item, long min = 0)
	{
		var value = readItem(item);

		if (value == null)
		{
			writeItem(item, min);
			return min;
		}

		return Convert.ToInt64(value);
	}

	public double readDouble(string item, double min = 0)
	{
		var value = readItem(item);

		if (value == null)
		{
			writeItem(item, 0);
			return min;
		}

		return Convert.ToDouble(value);
	}

	public string readString(string item, string normal = "")
	{
		var value = readItem(item);

		if (value == null)
		{
			writeItem(item, normal);
			return normal;
		}

		return Convert.ToString(value);
	}

	public void writeItem(string item, object value)
	{
		string cleanJson = "";

		if (!isJsonValid)
		{
			dynamic json = new ExpandoObject();
			var dictionary = (IDictionary<string, object>)json;
			dictionary.Add(item, value);
			cleanJson = JsonConvert.SerializeObject(dictionary);
		}
		else
		{
			var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(fileText);

			if (dictionary.ContainsKey(item))
				dictionary[item] = value;
			else
				dictionary.Add(item, value);

			cleanJson = JsonConvert.SerializeObject(dictionary);
		}

		File.WriteAllText(fileName, cleanJson);
		fileText = cleanJson;
		isJsonValid = true;
	}
}