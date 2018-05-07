using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI;
using WebUI.Pages;
using Xunit;

namespace demo42tests{
    public class IndexTests    {
        [Fact]
        public async Task Index_Post_Validates_Words(){
            var pageUnderTest = new IndexModel(new MockQueueClient(), new MockConfig());

            pageUnderTest.Data = "InvalidWordData";

            var result = await pageUnderTest.OnPost();

            Assert.Equal("Not saved. Nice try.", pageUnderTest.Message);
            Assert.IsAssignableFrom<Microsoft.AspNetCore.Mvc.RedirectToPageResult>(result);
        }
    }

    public class MockQueueClient : IQuoteClient{
        public Task<Quote> GetRandomQuote(){
            throw new Exception();
        }
    }

    public class MockConfig : IConfiguration{
        public string this[string key] { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }

        public IEnumerable<IConfigurationSection> GetChildren(){
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken(){
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key){
            throw new NotImplementedException();
        }
    }
}
