using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI;
using WebUI.Pages;
using Xunit;

namespace demo42tests
{
    public class BaseImageTests
    {
        [Fact]
        public void Base_Image_Validated_Expected()
        {
            var valueTested = Environment.GetEnvironmentVariable("SOMETHING_UNEXPECTED");
            Assert.True(string.IsNullOrEmpty(valueTested) ||
                        valueTested == "Good" ||
                        valueTested == "Wonderful";
        }
    }
}
