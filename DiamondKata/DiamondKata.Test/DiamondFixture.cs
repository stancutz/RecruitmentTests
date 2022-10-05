namespace DiamondKata.Test
{
    public class DiamontFixture
    {
        private DiamondService service;

        [SetUp]
        public void Setup()
        {
            service = new DiamondService();
        }

        [Test]
        [TestCase("A")]
        [TestCase("Q")]
        [TestCase("a")]
        [TestCase("w")]
        public void ValidateInput_Successfull(string input)
        {
            service.ValidateInput(input);

            Assert.Pass();
        }

        [Test]
        [TestCase("#")]
        [TestCase("@")]
        [TestCase("3")]
        [TestCase("7")]
        public void ValidateInput_SpecialCharacters(string input)
        {
            try
            {
                service.ValidateInput(input);
                Assert.Fail("no exception thrown");
            }
            catch (Exception expectedException)
            {
                Assert.IsTrue(expectedException is ArgumentException);
                Assert.That(expectedException.Message, Is.EqualTo("The input character is not a alphabet letter."));
            }
        }

        [Test]
        [TestCase("#123")]
        [TestCase("     ")]
        public void ValidateInput_MoreThanOneCharacter(string input)
        {
            try
            {
                service.ValidateInput(input);
                Assert.Fail("no exception thrown");
            }
            catch (Exception expectedException)
            {
                Assert.IsTrue(expectedException is ArgumentException);
                Assert.That(expectedException.Message, Is.EqualTo("Please insert only one character."));
            }
        }

        [Test]
        [TestCase(-10)]
        [TestCase( 1)]
        [TestCase(10)]
        [TestCase(26)]
        public void GetLineLength_Successfull(int input)
        {
            // expected value is odd number matematical progression
            var expected = (input <= 1) ? 1 : (1 + ((input - 1) * 2));

            var actual = service.GetLineLength(input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase( 1, "A")]
        [TestCase( 2, "B")]
        [TestCase(12, "L")]
        public void GetCurrentLetter_Successfull(int position, char expectedLetter)
        {
            var actual = service.GetCurrentLetter(position);

            Assert.That(actual, Is.EqualTo(expectedLetter));
        }


        [Test]
        [TestCase( 1,  1, "A")]
        [TestCase( 2,  3, "B_B")]
        [TestCase( 3,  5, "C___C")]
        [TestCase( 4,  7, "D_____D")]
        [TestCase( 5,  9, "E_______E")]
        [TestCase( 6, 11, "F_________F")]
        [TestCase( 7, 13, "G___________G")]
        [TestCase( 8, 15, "H_____________H")]
        [TestCase( 9, 17, "I_______________I")]
        [TestCase(10, 19, "J_________________J")]
        public void GetCurrentLine_Median(int currentIdx, int maxStringLength, string expected)
        {
            var actual = service.GetDiamondLine(currentIdx, maxStringLength);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(1, 3, "_A_")]
        [TestCase(1, 7, "___A___")]
        [TestCase(2, 7, "__B_B__")]
        [TestCase(3, 7, "_C___C_")]
        [TestCase(4, 7, "D_____D")]
        public void GetCurrentLine_Random(int currentIdx, int maxStringLength, string expected)
        {
            var actual = service.GetDiamondLine(currentIdx, maxStringLength);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}