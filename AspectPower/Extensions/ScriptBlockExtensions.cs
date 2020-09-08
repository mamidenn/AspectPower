using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;
using System.Management.Automation.Runspaces;

namespace AspectPower.Extensions
{
    public static class ScriptBlockExtensions
    {
        public static ScriptBlock ResolveAspects(this ScriptBlock scriptBlock)
        {
            var newAst = scriptBlock.Ast.Visit(new DuplicatingAstVisitor()) as ScriptBlockAst;
            return newAst.GetScriptBlock();
        }
    }
}