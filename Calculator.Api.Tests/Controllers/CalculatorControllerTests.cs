namespace Calculator.Api.Tests.Controllers
{
    using System;
    using Api.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Xunit;

    public class CalculatorControllerTests
    {
        private readonly CalculatorController _sut;

        public CalculatorControllerTests()
        {
            this._sut = new CalculatorController();
        }

        /// <summary>
        /// Tests that CalculatorController.Decimal returns the expected result.
        /// </summary>
        [Theory]
        [InlineData("add", 1, 2.33, 3.33)]
        [InlineData("subtract", 3.125, 2.12, 1.005)]
        [InlineData("multiply", 2, 3.3, 6.6)]
        [InlineData("divide", 6.5, 3.25, 2)]
        public void CalculatorController_Decimal_Pass(string op, decimal a, decimal b, decimal result)
        {
            // Execute SUT.
            IActionResult _result = this._sut.Decimal(op, a, b);

            // Verify Results.
            Assert.NotNull(_result);
            Assert.IsType<OkObjectResult>(_result);
            OkObjectResult _okResult = _result as OkObjectResult;
            Assert.NotNull(_okResult);
            Assert.IsType<DecimalModel>(_okResult.Value);
            DecimalModel _resultModel = _okResult.Value as DecimalModel;
            Assert.NotNull(_resultModel);
            Assert.True(_resultModel.Op.Equals(op, StringComparison.OrdinalIgnoreCase) &&
                        _resultModel.ValueA == a &&
                        _resultModel.ValueB == b &&
                        _resultModel.Result == result);
        }

        /// <summary>
        /// Tests that CalculatorController.Decimal returns the expected result.
        /// </summary>
        [Fact]
        public void CalculatorController_Decimal_Fail_InvalidOperator()
        {
            // Setup Fixtures.
            const string op = "foo";
            decimal? _a = 1;
            decimal? _b = 2;
            string _message = "Valid operators include: Add, Subtract, Multiply, and Divide.";

            // Execute SUT.
            IActionResult _result = this._sut.Decimal(op, _a, _b);

            // Verify Results.
            Assert.NotNull(_result);
            Assert.IsType<UnprocessableEntityObjectResult>(_result);
            UnprocessableEntityObjectResult _unprocessableEntityResult = _result as UnprocessableEntityObjectResult;
            Assert.NotNull(_unprocessableEntityResult?.Value);
            string _resultMessage = _unprocessableEntityResult.Value.ToString();
            Assert.Contains(_message, _resultMessage);
        }

        /// <summary>
        /// Tests that CalculatorController.Decimal returns the expected result.
        /// </summary>
        [Fact]
        public void CalculatorController_Decimal_Fail_InvalidAValue()
        {
            // Setup Fixtures.
            const string op = "subtract";
            decimal? _b = 2;
            const string message = "Invalid parameter. \"a\" must be an number.";

            // Execute SUT.
            IActionResult _result = this._sut.Decimal(op, null, _b);

            // Verify Results.
            Assert.NotNull(_result);
            Assert.IsType<UnprocessableEntityObjectResult>(_result);
            UnprocessableEntityObjectResult _unprocessableEntityResult = _result as UnprocessableEntityObjectResult;
            Assert.NotNull(_unprocessableEntityResult?.Value);
            string _resultMessage = _unprocessableEntityResult.Value.ToString();
            Assert.Contains(message, _resultMessage);
        }

        /// <summary>
        /// Tests that CalculatorController.Decimal returns the expected result.
        /// </summary>
        [Fact]
        public void CalculatorController_Decimal_Fail_InvalidBValue()
        {
            // Setup Fixtures.
            const string op = "multiply";
            decimal? _a = 1;
            string _message = "Invalid parameter. \"b\" must be an number.";

            // Execute SUT.
            IActionResult _result = this._sut.Decimal(op, _a, null);

            // Verify Results.
            Assert.NotNull(_result);
            Assert.IsType<UnprocessableEntityObjectResult>(_result);
            UnprocessableEntityObjectResult _unprocessableEntityResult = _result as UnprocessableEntityObjectResult;
            Assert.NotNull(_unprocessableEntityResult?.Value);
            string _resultMessage = _unprocessableEntityResult.Value.ToString();
            Assert.Contains(_message, _resultMessage);
        }

        /// <summary>
        /// Tests that CalculatorController.Integer returns the expected result.
        /// </summary>
        [Theory]
        [InlineData("add", 1, 2, 3)]
        [InlineData("subtract", 3, 2, 1)]
        [InlineData("multiply", 2, 3, 6)]
        [InlineData("divide", 6, 3, 2)]
        public void CalculatorController_Integer_Pass(string op, int a, int b, int result)
        {
            // Execute SUT.
            IActionResult _result = this._sut.Integer(op, a, b);

            // Verify Results.
            Assert.NotNull(_result);
            Assert.IsType<OkObjectResult>(_result);
            OkObjectResult _okResult = _result as OkObjectResult;
            Assert.NotNull(_okResult);
            Assert.IsType<IntegerModel>(_okResult.Value);
            IntegerModel _resultModel = _okResult.Value as IntegerModel;
            Assert.NotNull(_resultModel);
            Assert.True(_resultModel.Op.Equals(op, StringComparison.OrdinalIgnoreCase) &&
                        _resultModel.ValueA == a &&
                        _resultModel.ValueB == b &&
                        _resultModel.Result == result);
        }

        /// <summary>
        /// Tests that CalculatorController.Integer returns the expected result.
        /// </summary>
        [Fact]
        public void CalculatorController_Integer_Fail_InvalidOperator()
        {
            // Setup Fixtures.
            const string op = "foo";
            int? _a = 1;
            int? _b = 2;
            string _message = "Valid operators include: Add, Subtract, Multiply, and Divide.";

            // Execute SUT.
            IActionResult _result = this._sut.Integer(op, _a, _b);

            // Verify Results.
            Assert.NotNull(_result);
            Assert.IsType<UnprocessableEntityObjectResult>(_result);
            UnprocessableEntityObjectResult _unprocessableEntityResult = _result as UnprocessableEntityObjectResult;
            Assert.NotNull(_unprocessableEntityResult?.Value);
            string _resultMessage = _unprocessableEntityResult.Value.ToString();
            Assert.Contains(_message, _resultMessage);
        }

        /// <summary>
        /// Tests that CalculatorController.Integer returns the expected result.
        /// </summary>
        [Fact]
        public void CalculatorController_Integer_Fail_InvalidAValue()
        {
            // Setup Fixtures.
            const string op = "subtract";
            int? _b = 2;
            string _message = "Invalid parameter. \"a\" must be an number.";

            // Execute SUT.
            IActionResult _result = this._sut.Integer(op, null, _b);

            // Verify Results.
            Assert.NotNull(_result);
            Assert.IsType<UnprocessableEntityObjectResult>(_result);
            UnprocessableEntityObjectResult _unprocessableEntityResult = _result as UnprocessableEntityObjectResult;
            Assert.NotNull(_unprocessableEntityResult?.Value);
            string _resultMessage = _unprocessableEntityResult.Value.ToString();
            Assert.Contains(_message, _resultMessage);
        }

        /// <summary>
        /// Tests that CalculatorController.Integer returns the expected result.
        /// </summary>
        [Fact]
        public void CalculatorController_Integer_Fail_InvalidBValue()
        {
            // Setup Fixtures.
            const string op = "multiply";
            int? _a = 1;
            string _message = "Invalid parameter. \"b\" must be an number.";

            // Execute SUT.
            IActionResult _result = this._sut.Integer(op, _a, null);

            // Verify Results.
            Assert.NotNull(_result);
            Assert.IsType<UnprocessableEntityObjectResult>(_result);
            UnprocessableEntityObjectResult _unprocessableEntityResult = _result as UnprocessableEntityObjectResult;
            Assert.NotNull(_unprocessableEntityResult?.Value);
            string _resultMessage = _unprocessableEntityResult.Value.ToString();
            Assert.Contains(_message, _resultMessage);
        }
    }
}
