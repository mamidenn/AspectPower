function Test-Aspect {
    [AspectPower.Tests.TestAspect()]
    param(
        [Parameter(ValueFromPipeline)]
        $InputObject
    )

    process {
        Write-Output $InputObject
    }
}
