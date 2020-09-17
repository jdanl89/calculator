namespace Calculator.Api.Models
{
    /// <summary>
    /// The base interface for the models.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public interface IBaseModel<T>
    {
        /// <summary>
        /// Gets or sets value A.
        /// </summary>
        public T ValueA { get; set; }

        /// <summary>
        /// Gets or sets value B.
        /// </summary>
        public T ValueB { get; set; }

        /// <summary>
        /// Gets the result.
        /// </summary>
        public decimal Result { get; }

        /// <summary>
        /// Gets the name of the operator.
        /// </summary>
        public string Op { get; }
    }
}