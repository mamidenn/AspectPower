using System;
using System.Management.Automation;
using System.Management.Automation.Language;

namespace AspectPower
{
    public abstract class FunctionAspect : Attribute
    {
        public virtual object OnEnterBegin(InvocationInfo io) => null;
        public virtual object OnLeaveBegin(InvocationInfo io) => null;
        public virtual object OnEnterProcess(InvocationInfo io) => null;
        public virtual object OnLeaveProcess(InvocationInfo io) => null;
        public virtual object OnEnterEnd(InvocationInfo io) => null;
        public virtual object OnLeaveEnd(InvocationInfo io) => null;
    }
}