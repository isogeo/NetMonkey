using System;
using System.Threading.Tasks;
using Xunit;

namespace NetMonkey.Tests
{

    public static class AssertEx
    {

        public async static Task<T> ThrowsAsync<T>(Func<Task> testCode)
            where T: Exception
        {
            try
            {
                await testCode();
            } catch (T exception)
            {
                return exception;
            }

            Assert.Throws<T>(() => { }); // Use xUnit's default behavior.
            return null;
        }
    }
}
