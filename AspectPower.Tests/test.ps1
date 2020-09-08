using module './AspectPower/bin/Debug/netcoreapp3.1/AspectPower.dll'
$VerbosePreference = 'Continue'
# Import-Module $PSScriptRoot\..\AspectPower.psd1

class Trace : AspectPower.FunctionAspect {
    OnEnterProcess([System.Management.Automation.InvocationInfo] $invocationInfo) {
        Write-Verbose "-> $($invocationInfo.MyCommand.Name)"
    }
    OnLeaveProcess([System.Management.Automation.InvocationInfo] $invocationInfo) {
        Write-Verbose "<- $($invocationInfo.MyCommand.Name)"
    }
}

$a = { 
    function Hello {
        [CmdletBinding()]
        [Trace()]
        param(
            [Parameter(ValueFromPipeline)]
            $Foo
        )

        process {
            Write-Output $Foo
        }
    }
}

. (Resolve-Aspect $a)

'Foo', 'Bar', 'Baz' | Hello
