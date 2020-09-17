namespace Calculator.Api.Models
{
    using System.Text.Json.Serialization;
    using Enums;

    /// <summary>
    /// The response model for decimal requests.
    /// </summary>
    public class DecimalModel : BaseModel<decimal>, IBaseModel<decimal>
    {
        /// <summary>
        /// The Operator.
        /// </summary>
        private readonly Operator _operatorEnum;

        /// <summary>
        /// Generates a view model for the decimal view.
        /// </summary>
        /// <param name="a">Value A.</param>
        /// <param name="b">Value B.</param>
        /// <param name="op">The operator.</param>
        public DecimalModel(decimal a, decimal b, Operator op)
        {
            this.ValueA = a;
            this.ValueB = b;
            this._operatorEnum = op;
        }

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        [JsonPropertyName("operator")]
        public string Op => _operatorEnum.ToString().ToLower();

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        [JsonPropertyName("result")]
        public decimal Result => _operatorEnum switch
        {
            Operator.Add => ValueA + ValueB,
            Operator.Subtract => ValueA - ValueB,
            Operator.Multiply => ValueA * ValueB,
            Operator.Divide => ValueA / ValueB,
            _ => Result
        };
    }
}