namespace Calculator.Api.Models
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The base model.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public abstract class BaseModel<T>
    {
        /// <summary>
        /// Gets or sets value A.
        /// </summary>
        [JsonPropertyName("a")]
        public T ValueA { get; set; }

        /// <summary>
        /// Gets or sets value B.
        /// </summary>
        [JsonPropertyName("b")]
        public T ValueB { get; set; }
    }
}