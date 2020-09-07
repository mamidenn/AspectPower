using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Language;

namespace AspectPower
{
    public class FunctionVisitor : ScriptBlockVisitor
    {
        public override object VisitNamedBlock(NamedBlockAst namedBlockAst)
        {
            var newStatements = Enumerable.Empty<StatementAst>()
                .Append(AstFactory.GetWriteVerbose("→ $($MyInvocation.InvocationName)"))
                .Concat(VisitAst(namedBlockAst.Statements))
                .Append(AstFactory.GetWriteVerbose("← $($MyInvocation.InvocationName)"));

            var newTraps = VisitAst(namedBlockAst.Traps);

            return new NamedBlockAst(
                namedBlockAst.Extent,
                namedBlockAst.BlockKind,
                new StatementBlockAst(
                    namedBlockAst.Extent,
                    newStatements,
                    newTraps),
                namedBlockAst.Unnamed
            );
        }
    }
}