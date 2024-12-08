using System.Text.Json;
using System.Text.Json.Serialization;

namespace RapidPay.Domain;

/// <summary>
/// Defines the common configuration for serialize a class to json.
/// </summary>
public class JsonSerializerGlobalOptions
{
	/// <summary>
	/// Sets an existing serializer options with the common configuration that will apply for all json serialization.
	/// </summary>
	public static JsonSerializerOptions SetGlobalOptions(JsonSerializerOptions options)
	{
		options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
		options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
		options.PropertyNameCaseInsensitive = true;
		options.WriteIndented = true;

		_serializerOptions = options;
		return options;
	}

	/// <summary>
	/// Returns the <see cref="JsonSerializerOptions"/> that will be applied during serialization.
	/// </summary>
	public static JsonSerializerOptions SerializerOptions
	{
		get
		{
			if (_serializerOptions == null)
			{
				throw new ArgumentNullException(nameof(_serializerOptions), "The options are not initialized. Should call SetGlobalOptions");
			}

			return _serializerOptions;
		}
	}

	private static JsonSerializerOptions? _serializerOptions;
}
