using System.Management.Automation.Language;
using NUnit.Framework;
using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace AspectPower.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var input = @"function Hello {
    [AspectPower.Trace()]
    param()

    Write-Output 'Hello!'
}            
";
            var ast = Parser.ParseInput(input, out _, out _);
            var newAst = ast.GetScriptBlock().ResolveAspects();
            Assert.Pass();
        }
    }
}