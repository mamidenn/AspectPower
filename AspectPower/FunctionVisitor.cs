using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;

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
            var attributes = this.attributes.Select(GetAttributeType).ToList();
            var newStatements = new List<StatementAst>();
            for (int i = 0; i < attributes.Count; i++)
            {
                if (!IsFunctionAspect(attributes[i]))
                    continue;
                newStatements.Add(GetTransitionStatement(Transition.Enter, namedBlockAst.BlockKind, i));
            }
            newStatements.AddRange(VisitAst(namedBlockAst.Statements));
            for (int i = attributes.Count - 1; i >= 0; i--)
            {
                if (!IsFunctionAspect(attributes[i]))
                    continue;
                newStatements.Add(GetTransitionStatement(Transition.Leave, namedBlockAst.BlockKind, i));
            }

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
            return (ScriptBlock.Create(statement).Ast as ScriptBlockAst).EndBlock.Statements[0].Copy() as StatementAst;
        }

        private static Type GetAttributeType(AttributeAst a) => a.TypeName.GetReflectionAttributeType();

        private static bool IsFunctionAspect(Type t) => typeof(FunctionAspect).IsAssignableFrom(t);
    }
}