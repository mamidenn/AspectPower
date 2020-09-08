using System.Management.Automation.Language;
using NUnit.Framework;
using System.Management.Automation;
using AspectPower.Extensions;
using System.Linq;

namespace AspectPower.Tests
{
    public class TestAspect : FunctionAspect
    {

    }

    public class FunctionAspectTests
    {
        ScriptBlock scriptBlock;

        [SetUp]
        public void Setup()
        {
            scriptBlock = Parser.ParseFile("TestFunctions.ps1", out _, out _).GetScriptBlock();
        }

        [Test]
        public void WhenFunctionAspectAttributeIsPresent_TransitionStatementsAreInserted()
        {
            var actual = scriptBlock.ResolveAspects();
            var invokedMembers = actual.Ast.FindAll<InvokeMemberExpressionAst>(true).Select(a => ((StringConstantExpressionAst)a.Member).Value);
            Assert.That(invokedMembers, Is.EquivalentTo(new[] { "OnEnterProcess", "OnLeaveProcess" }));
        }
    }
}