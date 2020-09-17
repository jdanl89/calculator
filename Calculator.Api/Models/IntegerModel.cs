namespace Calculator.Api.Models
{
    using System.Text.Json.Serialization;
    using Enums;

    /// <summary>
    /// The response model for integer requests.
    /// </summary>
    public class IntegerModel : BaseModel<int>, IBaseModel<int>
    {
        /// <summary>
        /// The Operator.
        /// </summary>
        private readonly Operator _operatorEnum;

        /// <summary>
        /// Generates a view model for the Integer view.
        /// </summary>
        /// <param name="a">Value A.</param>
        /// <param name="b">Value B.</param>
        /// <param name="op">The operator.</param>
        public IntegerModel(int a, int b, Operator op)
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
        public decimal Result => this._operatorEnum switch
        {
            Operator.Add => ValueA + ValueB,
            Operator.Subtract => ValueA - ValueB,
            Operator.Multiply => ValueA * ValueB,
            Operator.Divide => (decimal)ValueA / ValueB,
            _ => Result
        };
    }
}