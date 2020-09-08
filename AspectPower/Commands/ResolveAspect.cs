using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using AspectPower.Extensions;

namespace AspectPower.Commands
{
    [Cmdlet(VerbsDiagnostic.Resolve, "Aspect")]
    [OutputType(typeof(ScriptBlock))]
    public class ResolveAspect : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public ScriptBlock ScriptBlock { get; set; }
        protected override void ProcessRecord()
        {
            WriteObject(ScriptBlock.ResolveAspects());
        }
    }
}
