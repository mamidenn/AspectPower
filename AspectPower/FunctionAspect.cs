using System;
using System.Management.Automation;

namespace AspectPower
{
    public abstract class FunctionAspect : Attribute
    {
        public virtual void OnEnterBegin(InvocationInfo invocationInfo) { }
        public virtual void OnLeaveBegin(InvocationInfo invocationInfo) { }
        public virtual void OnEnterProcess(InvocationInfo invocationInfo) { }
        public virtual void OnLeaveProcess(InvocationInfo invocationInfo) { }
        public virtual void OnEnterEnd(InvocationInfo invocationInfo) { }
        public virtual void OnLeaveEnd(InvocationInfo invocationInfo) { }
    }
}