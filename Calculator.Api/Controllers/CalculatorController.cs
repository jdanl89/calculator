namespace Calculator.Api.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Enums;
    using Models;

    /// <summary>
    /// The controller class containing the calculator endpoints.s
    /// </summary>
    public class CalculatorController : ControllerBase
    {
        /// <summary>
        /// Conducts the designated operation on the given values.
        /// </summary>
        /// <param name="op">The mathematical operator.</param>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <returns>An object containing the result of the mathematical computation</returns>
        [HttpGet]
        [Route("api/calculator/decimal/{op}")]
        public IActionResult Decimal(string op, decimal? a, decimal? b)
        {
            (bool _valid, IActionResult _validationResult, Operator _operator) = this.Validate(op, a, b);
            if (!_valid) { return _validationResult; }

            return this.Ok(new DecimalModel(a.Value, b.Value, _operator));
        }

        /// <summary>
        /// Conducts the designated operation on the given values.
        /// </summary>
        /// <param name="op">The mathematical operator.</param>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <returns>An object containing the result of the mathematical computation</returns>
        [HttpGet]
        [Route("api/calculator/integer/{op}")]
        public IActionResult Integer(string op, int? a, int? b)
        {
            (bool _valid, IActionResult _validationResult, Operator _operator) = this.Validate(op, a, b);
            if (!_valid) { return _validationResult; }

            return this.Ok(new IntegerModel(a.Value, b.Value, _operator));
        }

        /// <summary>
        /// Validates the incoming parameters.
        /// </summary>
        /// <typeparam name="T">The type of the numeric values.</typeparam>
        /// <param name="op">The mathematical operator.</param>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <returns></returns>
        private (bool valid, IActionResult result, Operator op) Validate<T>(string op, T a, T b)
        {
            if (!Enum.TryParse(op, true, out Operator _operator))
            {
                return (
                    false,
                    this.UnprocessableEntity(new
                    {
                        Message = $"Invalid parameter \"op\". {op} is not a valid operator. Valid operators include: Add, Subtract, Multiply, and Divide."
                    }),
                    0);
            }

            if (a is null)
            {
                return (
                    false,
                    this.UnprocessableEntity(new
                    {
                        Message = "Invalid parameter. \"a\" must be an number."
                    }),
                    0);
            }

            if (b is null)
            {
                return (
                    false,
                    this.UnprocessableEntity(new
                    {
                        Message = "Invalid parameter. \"b\" must be an number."
                    }),
                    0);
            }

            return (true, null as IActionResult, _operator);
        }
    }
}
