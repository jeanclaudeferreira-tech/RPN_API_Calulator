using RpnCalculator_V1;
using NFluent;
namespace TestRPN
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void should_throws_exception_if_empty()
        {
            RPNCalc calc = new RPNCalc();
            Check.ThatCode(() => calc.Calc(""))
            .Throws<ArgumentException>().WithMessage("Input should not be empty");
        }

        [Test]
        public void should_throws_exception_if_one_operand()
        {
            RPNCalc calc = new RPNCalc();
            Check.ThatCode(() => calc.Calc("1"))
            .Throws<ArgumentException>().WithMessage("Input must contain at least 3 items");
        }

        [Test]
        public void should_throws_exception_if_two_operand()
        {
            RPNCalc calc = new RPNCalc();
            Check.ThatCode(() => calc.Calc("1 2"))
            .Throws<ArgumentException>().WithMessage("Input must contain at least 3 items");
        }

        [Test]
        public void should_return_0_if_input_0_plus_0()
        {
            RPNCalc calc = new RPNCalc();
            var result = calc.Calc("0 0 +");
            Check.That(result).IsEqualTo(0.0);
        }

        [Test]
        public void should_return_5_if_input_2_plus_3()
        {
            RPNCalc calc = new RPNCalc();
            var result = calc.Calc("2 3 +");
            Check.That(result).IsEqualTo(5.0);
        }

        [Test]
        public void should_return_6_if_input_2_multiply_3()
        {
            RPNCalc calc = new RPNCalc();
            var result = calc.Calc("2 3 *");
            Check.That(result).IsEqualTo(6.0);
        }

        [Test]
        public void should_return_4()
        {
            RPNCalc calc = new RPNCalc();
            var result = calc.Calc("2 4 + 7 2 - * 9 6 + / 2 +");
            Check.That(result).IsEqualTo(4.0);
        }

    }
}