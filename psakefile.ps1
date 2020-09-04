Task default -depends test 

Task build {
    Exec { dotnet build }
}

Task test -depends build {
    Exec { dotnet test }
    Exec { pwsh -Command { Invoke-Pester -EnableExit } }
}