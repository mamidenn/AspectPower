using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;

namespace AspectPower
{
    public class FunctionVisitor : ScriptBlockVisitor
    {
        private readonly FunctionDefinitionAst functionDefinitionAst;
        private readonly IEnumerable<AttributeAst> attributes;

        public FunctionVisitor(FunctionDefinitionAst functionDefinitionAst)
        {
            this.functionDefinitionAst = functionDefinitionAst;
            attributes = functionDefinitionAst.Body.ParamBlock.Attributes;
        }

        public override object VisitNamedBlock(NamedBlockAst namedBlockAst)
        {
            var functionAspects = attributes.Select(GetAttributeType).Where(IsFunctionAspect);
            var attribute = functionAspects.First();

            // foreach (var attribute in functionAspects)
            // {
            StatementAst onEnter;
            StatementAst onLeave;
            // switch (namedBlockAst.BlockKind)
            // {
            //     case TokenKind.Begin:
            //         break;
            //     case TokenKind.Process:
            //         break;
            //     case TokenKind.End:
            onEnter = GetStatementAst($"$MyInvocation.MyCommand.ScriptBlock.Attributes.Where( {{ $_ -is [{attribute.FullName}] }}).OnEnterEnd($MyInvocation) | Write-Output");
            onLeave = GetStatementAst($"$MyInvocation.MyCommand.ScriptBlock.Attributes.Where( {{ $_ -is [{attribute.FullName}] }}).OnLeaveEnd($MyInvocation) | Write-Output");
            //     break;
            // default:
            //     throw new Exception();
            // }
            // }
            var newStatements = Enumerable.Empty<StatementAst>()
                .Append(onEnter)
                .Concat(VisitAst(namedBlockAst.Statements))
                .Append(onLeave);

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

        private static StatementAst GetStatementAst(string statement)
        {
            return (ScriptBlock.Create(statement).Ast as ScriptBlockAst).EndBlock.Statements[0].Copy() as StatementAst;
        }

        private static Type GetAttributeType(AttributeAst a) => a.TypeName.GetReflectionAttributeType();

        private static bool IsFunctionAspect(Type t) => typeof(FunctionAspect).IsAssignableFrom(t);
    }
}