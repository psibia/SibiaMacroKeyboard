using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SibiaMacroKeyboard.Tests
{
    [TestClass]
    public class SibiaMacroKeyboardTests
    {
        [TestMethod]
        public void ToEnum_SHIFT_return_16()
        {
            // arrange
            string text = "SHIFT";
            int expected = 16;


            // action
            int actual = Convert.ToInt32(ConverterStringToEnum.ToEnum<WindowsInput.Native.VirtualKeyCode>("SHIFT"));

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
