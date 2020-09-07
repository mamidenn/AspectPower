$VerbosePreference = 'Continue'
Import-Module $PSScriptRoot\..\AspectPower.psd1

class Trace : AspectPower.FunctionAspect {
    [object] OnEnterEnd([System.Management.Automation.InvocationInfo] $invocationInfo) {
        Write-Verbose "-> $($invocationInfo.MyCommand.Name)"
        return $null
    }
    [object] OnLeaveEnd([System.Management.Automation.InvocationInfo] $invocationInfo) {
        Write-Verbose "<- $($invocationInfo.MyCommand.Name)"
        return $null
    }
}

$a = { 
    function Hello {
        [Trace()]
        [CmdletBinding()]
        param()

        Write-Output 'Hello!'
    }
}

. (Resolve-Aspect $a)

Hello