using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Language;

namespace AspectPower.Extensions
{
    public static class AstExtensions
    {
        public static IEnumerable<T> FindAll<T>(this Ast ast, bool searchNestedScriptBlocks)
        {
            return ast.FindAll(a => a is T, searchNestedScriptBlocks).Cast<T>();
        }
    }
}