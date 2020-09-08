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
    [CmdletBinding()]
    [AspectPower.FunctionAspect()]
    param(
        [Parameter(ValueFromPipeline)]
        $Foo
    )

    process {
        Write-Output $Foo
    }
}
";
            var ast = Parser.ParseInput(input, out _, out _);
            var newAst = ast.GetScriptBlock().ResolveAspects();
            Assert.Pass();
        }
    }
}