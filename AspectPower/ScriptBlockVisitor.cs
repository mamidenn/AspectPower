using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation.Language;

namespace AspectPower
{
    public class ScriptBlockVisitor : ICustomAstVisitor2
    {
        protected T VisitAst<T>(T ast) where T : Ast =>
            ast?.Visit(this) as T;

        protected IEnumerable<T> VisitAst<T>(IEnumerable<T> asts) where T : Ast =>
            asts?.Select(VisitAst);

        public object VisitArrayExpression(ArrayExpressionAst arrayExpressionAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitArrayLiteral(ArrayLiteralAst arrayLiteralAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitAssignmentStatement(AssignmentStatementAst assignmentStatementAst)
        {
            var left = VisitAst(assignmentStatementAst.Left);
            var right = VisitAst(assignmentStatementAst.Right);
            return new AssignmentStatementAst(assignmentStatementAst.Extent,
                left, assignmentStatementAst.Operator, right,
                assignmentStatementAst.ErrorPosition);
        }

        public object VisitAttribute(AttributeAst attributeAst)
        {
            var named = VisitAst(attributeAst.NamedArguments);
            var positional = VisitAst(attributeAst.PositionalArguments);
            return new AttributeAst(attributeAst.Extent, attributeAst.TypeName,
                positional, named);
        }

        public object VisitAttributedExpression(AttributedExpressionAst attributedExpressionAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitBinaryExpression(BinaryExpressionAst binaryExpressionAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitBlockStatement(BlockStatementAst blockStatementAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitBreakStatement(BreakStatementAst breakStatementAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitCatchClause(CatchClauseAst catchClauseAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitCommand(CommandAst commandAst)
        {
            var elements = VisitAst(commandAst.CommandElements);
            var redirections = VisitAst(commandAst.Redirections);
            return new CommandAst(commandAst.Extent, elements, commandAst.InvocationOperator,
                redirections);
        }

        public object VisitCommandExpression(CommandExpressionAst commandExpressionAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitCommandParameter(CommandParameterAst commandParameterAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitConstantExpression(ConstantExpressionAst constantExpressionAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitContinueStatement(ContinueStatementAst continueStatementAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitConvertExpression(ConvertExpressionAst convertExpressionAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitDataStatement(DataStatementAst dataStatementAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitDoUntilStatement(DoUntilStatementAst doUntilStatementAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitDoWhileStatement(DoWhileStatementAst doWhileStatementAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitErrorExpression(ErrorExpressionAst errorExpressionAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitErrorStatement(ErrorStatementAst errorStatementAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitExitStatement(ExitStatementAst exitStatementAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitExpandableStringExpression(ExpandableStringExpressionAst expandableStringExpressionAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitFileRedirection(FileRedirectionAst fileRedirectionAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitForEachStatement(ForEachStatementAst forEachStatementAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitForStatement(ForStatementAst forStatementAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitFunctionDefinition(FunctionDefinitionAst functionDefinitionAst)
        {
            var body = functionDefinitionAst.Body?.Visit(new FunctionVisitor(functionDefinitionAst)) as ScriptBlockAst;
            var parameters = VisitAst(functionDefinitionAst.Parameters);
            return new FunctionDefinitionAst(functionDefinitionAst.Extent,
                functionDefinitionAst.IsFilter, functionDefinitionAst.IsWorkflow,
                functionDefinitionAst.Name, parameters, body);
        }

        public object VisitHashtable(HashtableAst hashtableAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitIfStatement(IfStatementAst ifStmtAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitIndexExpression(IndexExpressionAst indexExpressionAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitInvokeMemberExpression(InvokeMemberExpressionAst invokeMemberExpressionAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitMemberExpression(MemberExpressionAst memberExpressionAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitMergingRedirection(MergingRedirectionAst mergingRedirectionAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitNamedAttributeArgument(NamedAttributeArgumentAst namedAttributeArgumentAst)
        {
            throw new System.NotImplementedException();
        }

        public virtual object VisitNamedBlock(NamedBlockAst namedBlockAst)
        {
            var statements = VisitAst(namedBlockAst.Statements).ToList();
            var traps = VisitAst(namedBlockAst.Traps);
            return new NamedBlockAst(
                namedBlockAst.Extent,
                namedBlockAst.BlockKind,
                new StatementBlockAst(
                    namedBlockAst.Extent,
                    statements,
                    traps),
                namedBlockAst.Unnamed);
        }

        public object VisitParamBlock(ParamBlockAst paramBlockAst)
        {
            var attributes = VisitAst(paramBlockAst.Attributes);
            var parameters = VisitAst(paramBlockAst.Parameters);
            return new ParamBlockAst(paramBlockAst.Extent, attributes, parameters);
        }

        public object VisitParameter(ParameterAst parameterAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitParenExpression(ParenExpressionAst parenExpressionAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitPipeline(PipelineAst pipelineAst)
        {
            var elements = VisitAst(pipelineAst.PipelineElements);
            return new PipelineAst(pipelineAst.Extent, elements);
        }

        public object VisitReturnStatement(ReturnStatementAst returnStatementAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitScriptBlock(ScriptBlockAst scriptBlockAst)
        {
            var param = VisitAst(scriptBlockAst.ParamBlock);
            var begin = VisitAst(scriptBlockAst.BeginBlock);
            var process = VisitAst(scriptBlockAst.ProcessBlock);
            var end = VisitAst(scriptBlockAst.EndBlock);
            var dynamicParam = VisitAst(scriptBlockAst.DynamicParamBlock);
            return new ScriptBlockAst(scriptBlockAst.Extent, param, begin, process,
                end, dynamicParam);
        }

        public object VisitScriptBlockExpression(ScriptBlockExpressionAst scriptBlockExpressionAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitStatementBlock(StatementBlockAst statementBlockAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitStringConstantExpression(StringConstantExpressionAst stringConstantExpressionAst)
        {
            return new StringConstantExpressionAst(stringConstantExpressionAst.Extent,
                stringConstantExpressionAst.Value, stringConstantExpressionAst.StringConstantType);
        }

        public object VisitSubExpression(SubExpressionAst subExpressionAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitSwitchStatement(SwitchStatementAst switchStatementAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitThrowStatement(ThrowStatementAst throwStatementAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitTrap(TrapStatementAst trapStatementAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitTryStatement(TryStatementAst tryStatementAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitTypeConstraint(TypeConstraintAst typeConstraintAst)
        {
            return typeConstraintAst.Copy();
        }

        public object VisitTypeExpression(TypeExpressionAst typeExpressionAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitUnaryExpression(UnaryExpressionAst unaryExpressionAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitUsingExpression(UsingExpressionAst usingExpressionAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitVariableExpression(VariableExpressionAst variableExpressionAst)
        {
            return variableExpressionAst.Copy();
        }

        public object VisitWhileStatement(WhileStatementAst whileStatementAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitBaseCtorInvokeMemberExpression(BaseCtorInvokeMemberExpressionAst baseCtorInvokeMemberExpressionAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitConfigurationDefinition(ConfigurationDefinitionAst configurationDefinitionAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitDynamicKeywordStatement(DynamicKeywordStatementAst dynamicKeywordAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitFunctionMember(FunctionMemberAst functionMemberAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitPropertyMember(PropertyMemberAst propertyMemberAst)
        {
            throw new System.NotImplementedException();
        }

        public object VisitTypeDefinition(TypeDefinitionAst typeDefinitionAst)
        {
            var attributes = VisitAst(typeDefinitionAst.Attributes);
            var baseTypes = VisitAst(typeDefinitionAst.BaseTypes);
            var members = VisitAst(typeDefinitionAst.Members);
            return new TypeDefinitionAst(typeDefinitionAst.Extent, typeDefinitionAst.Name,
                attributes, members, typeDefinitionAst.TypeAttributes, baseTypes);
        }

        public object VisitUsingStatement(UsingStatementAst usingStatement)
        {
            throw new System.NotImplementedException();
        }
    }
}