using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Language;

namespace AspectPower
{
    public class DuplicatingAstVisitor : ICustomAstVisitor2
    {
        protected T VisitAst<T>(T ast) where T : Ast =>
            ast?.Visit(this) as T;

        protected IEnumerable<T> VisitAst<T>(IEnumerable<T> asts) where T : Ast =>
            asts?.Select(VisitAst);

        public object VisitArrayExpression(ArrayExpressionAst arrayExpressionAst)
        {
            var statementBlock = VisitAst(arrayExpressionAst.SubExpression);
            return new ArrayExpressionAst(arrayExpressionAst.Extent, statementBlock);
        }

        public object VisitArrayLiteral(ArrayLiteralAst arrayLiteralAst)
        {
            var elements = VisitAst(arrayLiteralAst.Elements).ToList();
            return new ArrayLiteralAst(arrayLiteralAst.Extent, elements);
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
            var attribute = VisitAst(attributedExpressionAst.Attribute);
            var child = VisitAst(attributedExpressionAst.Child);
            return new AttributedExpressionAst(attributedExpressionAst.Extent, attribute, child);
        }

        public object VisitBinaryExpression(BinaryExpressionAst binaryExpressionAst)
        {
            var left = VisitAst(binaryExpressionAst.Left);
            var right = VisitAst(binaryExpressionAst.Right);
            return new BinaryExpressionAst(binaryExpressionAst.Extent, left, binaryExpressionAst.Operator, right, 
                binaryExpressionAst.ErrorPosition);
        }

        public object VisitBlockStatement(BlockStatementAst blockStatementAst)
        {
            var body = VisitAst(blockStatementAst.Body);
            return new BlockStatementAst(blockStatementAst.Extent, blockStatementAst.Kind, body);
        }

        public object VisitBreakStatement(BreakStatementAst breakStatementAst)
        {
            return breakStatementAst.Copy();
        }

        public object VisitCatchClause(CatchClauseAst catchClauseAst)
        {
            var catchTypes = VisitAst(catchClauseAst.CatchTypes);
            var body = VisitAst(catchClauseAst.Body);
            return new CatchClauseAst(catchClauseAst.Extent, catchTypes, body);
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
            var expression = VisitAst(commandExpressionAst.Expression);
            var redirections = VisitAst(commandExpressionAst.Redirections);
            return new CommandExpressionAst(commandExpressionAst.Extent, expression, redirections);
        }

        public object VisitCommandParameter(CommandParameterAst commandParameterAst)
        {
            var argument = VisitAst(commandParameterAst.Argument);
            return new CommandParameterAst(commandParameterAst.Extent, commandParameterAst.ParameterName, argument, 
                commandParameterAst.ErrorPosition);
        }

        public object VisitConstantExpression(ConstantExpressionAst constantExpressionAst)
        {
            return constantExpressionAst.Copy();
        }

        public object VisitContinueStatement(ContinueStatementAst continueStatementAst)
        {
            var label = VisitAst(continueStatementAst.Label);
            return new ContinueStatementAst(continueStatementAst.Extent, label);
        }

        public object VisitConvertExpression(ConvertExpressionAst convertExpressionAst)
        {
            var typeConstraint = VisitAst(convertExpressionAst.Type);
            var child = VisitAst(convertExpressionAst.Child);
            return new ConvertExpressionAst(convertExpressionAst.Extent, typeConstraint, child);
        }

        public object VisitDataStatement(DataStatementAst dataStatementAst)
        {
            var commandsAllowed = VisitAst(dataStatementAst.CommandsAllowed);
            var body = VisitAst(dataStatementAst.Body);
            return new DataStatementAst(dataStatementAst.Extent, dataStatementAst.Variable, commandsAllowed, body);
        }

        public object VisitDoUntilStatement(DoUntilStatementAst doUntilStatementAst)
        {
            var condition = VisitAst(doUntilStatementAst.Condition);
            var body = VisitAst(doUntilStatementAst.Body);
            return new DoUntilStatementAst(doUntilStatementAst.Extent, doUntilStatementAst.Label, condition, body);
        }

        public object VisitDoWhileStatement(DoWhileStatementAst doWhileStatementAst)
        {
            var condition = VisitAst(doWhileStatementAst.Condition);
            var body = VisitAst(doWhileStatementAst.Body);
            return new DoUntilStatementAst(doWhileStatementAst.Extent, doWhileStatementAst.Label, condition, body);
        }

        public object VisitErrorExpression(ErrorExpressionAst errorExpressionAst)
        {
            // TODO: why is there no public constructor for ast with nested asts?
            return errorExpressionAst.Copy();
        }

        public object VisitErrorStatement(ErrorStatementAst errorStatementAst)
        {
            // TODO: why is there no public constructor for ast with nested asts?
            return errorStatementAst.Copy();
        }

        public object VisitExitStatement(ExitStatementAst exitStatementAst)
        {
            var pipeline = VisitAst(exitStatementAst.Pipeline);
            return new ExitStatementAst(exitStatementAst.Extent, pipeline);
        }

        public object VisitExpandableStringExpression(ExpandableStringExpressionAst expandableStringExpressionAst)
        {
            return expandableStringExpressionAst.Copy();
        }

        public object VisitFileRedirection(FileRedirectionAst fileRedirectionAst)
        {
            var file = VisitAst(fileRedirectionAst.Location);
            return new FileRedirectionAst(fileRedirectionAst.Extent, fileRedirectionAst.FromStream, file, fileRedirectionAst.Append);
        }

        public object VisitForEachStatement(ForEachStatementAst forEachStatementAst)
        {
            var throttleLimit = VisitAst(forEachStatementAst.ThrottleLimit);
            var variable = VisitAst(forEachStatementAst.Variable);
            var expression = VisitAst(forEachStatementAst.Condition);
            var body = VisitAst(forEachStatementAst.Body);
            return new ForEachStatementAst(forEachStatementAst.Extent, forEachStatementAst.Label, forEachStatementAst.Flags, 
                throttleLimit, variable, expression, body);
        }

        public object VisitForStatement(ForStatementAst forStatementAst)
        {
            var initializer = VisitAst(forStatementAst.Initializer);
            var condition = VisitAst(forStatementAst.Condition);
            var iterator = VisitAst(forStatementAst.Iterator);
            var body = VisitAst(forStatementAst.Body);
            return new ForStatementAst(forStatementAst.Extent, forStatementAst.Label, initializer, condition, iterator, body);
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
            return hashtableAst.Copy();
        }

        public object VisitIfStatement(IfStatementAst ifStmtAst)
        {
            var elseClause = VisitAst(ifStmtAst.ElseClause);
            var clauses = ifStmtAst.Clauses.Select(t => Tuple.Create(VisitAst(t.Item1), VisitAst(t.Item2)));
            return new IfStatementAst(ifStmtAst.Extent, clauses, elseClause);
        }

        public object VisitIndexExpression(IndexExpressionAst indexExpressionAst)
        {
            var target = VisitAst(indexExpressionAst.Target);
            var index = VisitAst(indexExpressionAst.Index);
            return new IndexExpressionAst(indexExpressionAst.Extent, target, index);
        }

        public object VisitInvokeMemberExpression(InvokeMemberExpressionAst invokeMemberExpressionAst)
        {
            var expression = VisitAst(invokeMemberExpressionAst.Expression);
            var method = VisitAst(invokeMemberExpressionAst.Member);
            var arguments = VisitAst(invokeMemberExpressionAst.Arguments);
            return new InvokeMemberExpressionAst(invokeMemberExpressionAst.Extent, expression, method, arguments, 
                invokeMemberExpressionAst.Static);
        }

        public object VisitMemberExpression(MemberExpressionAst memberExpressionAst)
        {
            var expression = VisitAst(memberExpressionAst.Expression);
            var member = VisitAst(memberExpressionAst.Member);
            return new MemberExpressionAst(memberExpressionAst.Extent, expression, member, memberExpressionAst.Static);
        }

        public object VisitMergingRedirection(MergingRedirectionAst mergingRedirectionAst)
        {
            return mergingRedirectionAst.Copy();
        }

        public object VisitNamedAttributeArgument(NamedAttributeArgumentAst namedAttributeArgumentAst)
        {
            var argument = VisitAst(namedAttributeArgumentAst.Argument);
            return new NamedAttributeArgumentAst(namedAttributeArgumentAst.Extent, namedAttributeArgumentAst.ArgumentName, 
                argument, namedAttributeArgumentAst.ExpressionOmitted);
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
            var attributes = VisitAst(parameterAst.Attributes);
            var defaultValue = VisitAst(parameterAst.DefaultValue);
            var name = VisitAst(parameterAst.Name);
            return new ParameterAst(parameterAst.Extent, name, attributes, defaultValue);
        }

        public object VisitParenExpression(ParenExpressionAst parenExpressionAst)
        {
            var pipeline = VisitAst(parenExpressionAst.Pipeline);
            return new ParenExpressionAst(parenExpressionAst.Extent, pipeline);
        }

        public object VisitPipeline(PipelineAst pipelineAst)
        {
            var elements = VisitAst(pipelineAst.PipelineElements);
            return new PipelineAst(pipelineAst.Extent, elements);
        }

        public object VisitReturnStatement(ReturnStatementAst returnStatementAst)
        {
            var pipeline = VisitAst(returnStatementAst.Pipeline);
            return new ReturnStatementAst(returnStatementAst.Extent, pipeline);
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
            var scriptBlock = VisitAst(scriptBlockExpressionAst.ScriptBlock);
            return new ScriptBlockExpressionAst(scriptBlockExpressionAst.Extent, scriptBlock);
        }

        public object VisitStatementBlock(StatementBlockAst statementBlockAst)
        {
            var statements = VisitAst(statementBlockAst.Statements);
            var traps = VisitAst(statementBlockAst.Traps);
            return new StatementBlockAst(statementBlockAst.Extent, statements, traps);
        }

        public object VisitStringConstantExpression(StringConstantExpressionAst stringConstantExpressionAst)
        {
            return new StringConstantExpressionAst(stringConstantExpressionAst.Extent,
                stringConstantExpressionAst.Value, stringConstantExpressionAst.StringConstantType);
        }

        public object VisitSubExpression(SubExpressionAst subExpressionAst)
        {
            var statementBlock = VisitAst(subExpressionAst.SubExpression);
            return new SubExpressionAst(subExpressionAst.Extent, statementBlock);
        }

        public object VisitSwitchStatement(SwitchStatementAst switchStatementAst)
        {
            var condition = VisitAst(switchStatementAst.Condition);
            var clauses = switchStatementAst.Clauses.Select(c => Tuple.Create(c.Item1, c.Item2));
            var @default = VisitAst(switchStatementAst.Default);
            return new SwitchStatementAst(switchStatementAst.Extent, switchStatementAst.Label, condition, switchStatementAst.Flags,
                clauses, @default);
        }

        public object VisitThrowStatement(ThrowStatementAst throwStatementAst)
        {
            var pipeline = VisitAst(throwStatementAst.Pipeline);
            return new ThrowStatementAst(throwStatementAst.Extent, pipeline);
        }

        public object VisitTrap(TrapStatementAst trapStatementAst)
        {
            var trapType = VisitAst(trapStatementAst.TrapType);
            var body = VisitAst(trapStatementAst.Body);
            return new TrapStatementAst(trapStatementAst.Extent, trapType, body);
        }

        public object VisitTryStatement(TryStatementAst tryStatementAst)
        {
            var body = VisitAst(tryStatementAst.Body);
            var catchClauses = VisitAst(tryStatementAst.CatchClauses);
            var @finally = VisitAst(tryStatementAst.Finally);
            return new TryStatementAst(tryStatementAst.Extent, body, catchClauses, @finally);
        }

        public object VisitTypeConstraint(TypeConstraintAst typeConstraintAst)
        {
            return typeConstraintAst.Copy();
        }

        public object VisitTypeExpression(TypeExpressionAst typeExpressionAst)
        {
            return typeExpressionAst.Copy();
        }

        public object VisitUnaryExpression(UnaryExpressionAst unaryExpressionAst)
        {
            var child = VisitAst(unaryExpressionAst.Child);
            return new UnaryExpressionAst(unaryExpressionAst.Extent, unaryExpressionAst.TokenKind, child);
        }

        public object VisitUsingExpression(UsingExpressionAst usingExpressionAst)
        {
            var expressionAst = VisitAst(usingExpressionAst.SubExpression);
            return new UsingExpressionAst(usingExpressionAst.Extent, expressionAst);
        }

        public object VisitVariableExpression(VariableExpressionAst variableExpressionAst)
        {
            return variableExpressionAst.Copy();
        }

        public object VisitWhileStatement(WhileStatementAst whileStatementAst)
        {
            var condition = VisitAst(whileStatementAst.Condition);
            var body = VisitAst(whileStatementAst.Body);
            return new WhileStatementAst(whileStatementAst.Extent, whileStatementAst.Label, condition, body);
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
