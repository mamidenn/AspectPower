using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;
using AspectPower.Extensions;

namespace AspectPower
{
    public class FunctionVisitor : DuplicatingAstVisitor
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
            var statements = AddTransitionStatements(namedBlockAst.Statements, namedBlockAst.BlockKind);
            var traps = VisitAst(namedBlockAst.Traps);

            return new NamedBlockAst(
                namedBlockAst.Extent,
                namedBlockAst.BlockKind,
                new StatementBlockAst(
                    namedBlockAst.Extent,
                    statements,
                    traps),
                namedBlockAst.Unnamed
            );
        }

        private IEnumerable<StatementAst> AddTransitionStatements(IEnumerable<StatementAst> statements, TokenKind blockKind)
        {
            var attributes = this.attributes.Select(GetAttributeType).ToList();
            var newStatements = new List<StatementAst>();
            for (int i = 0; i < attributes.Count; i++)
            {
                if (attributes[i].Is<FunctionAspect>())
                    newStatements.Add(GetTransitionStatement(Transition.Enter, blockKind, i));
            }
            newStatements.AddRange(VisitAst(statements));
            for (int i = attributes.Count - 1; i >= 0; i--)
            {
                if (attributes[i].Is<FunctionAspect>())
                    newStatements.Add(GetTransitionStatement(Transition.Leave, blockKind, i));
            }

            return newStatements;
        }

        enum Transition
        {
            Enter,
            Leave
        }

        private static StatementAst GetTransitionStatement(Transition transition, TokenKind blockKind, int attributeIndex)
        {
            return GetStatementAst($"$MyInvocation.MyCommand.ScriptBlock.Attributes[{attributeIndex}].On{transition}{blockKind}($MyInvocation)");
        }

        private static StatementAst GetStatementAst(string statement)
        {
            return (ScriptBlock.Create(statement).Ast as ScriptBlockAst).EndBlock.Statements.Single().Copy() as StatementAst;
        }

        private static Type GetAttributeType(AttributeAst a) => a.TypeName.GetReflectionAttributeType();
    }
}