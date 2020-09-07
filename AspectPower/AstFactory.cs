using System.Linq;
using System.Management.Automation.Language;

namespace AspectPower
{
    public class AstFactory
    {
        private static readonly ScriptExtent zeroExtent = new ScriptExtent(
                new ScriptPosition(string.Empty, 0, 0, string.Empty),
                new ScriptPosition(string.Empty, 0, 0, string.Empty)
            );

        public static PipelineAst GetWriteVerbose(string message)
        {
            return new PipelineAst(
                zeroExtent,
                new CommandAst(
                    zeroExtent,
                    new CommandElementAst[]{
                        new StringConstantExpressionAst(zeroExtent, "Write-Verbose", StringConstantType.BareWord),
                        new ExpandableStringExpressionAst(zeroExtent, message, StringConstantType.DoubleQuoted)
                    },
                    TokenKind.Unknown,
                    Enumerable.Empty<RedirectionAst>()
                ));
        }
    }
}