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
            var newStatements = new List<StatementAst>();

            var zeroExtent = new ScriptExtent(
                new ScriptPosition(string.Empty, 0, 0, string.Empty),
                new ScriptPosition(string.Empty, 0, 0, string.Empty)
            );
            var logStatement = new PipelineAst(
                zeroExtent,
                new CommandAst(
                    zeroExtent,
                    new[]{
                        new StringConstantExpressionAst(zeroExtent, "Write-Output", StringConstantType.BareWord),
                        new StringConstantExpressionAst(zeroExtent, "AST IS WORKING!", StringConstantType.SingleQuoted)
                    },
                    TokenKind.Unknown,
                    Enumerable.Empty<RedirectionAst>()
                ));

            newStatements.Add(logStatement);
            newStatements.AddRange(VisitAst(namedBlockAst.Statements));

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