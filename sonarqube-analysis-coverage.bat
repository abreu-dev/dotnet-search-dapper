dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
dotnet sonarscanner begin /k:"DOTNETSEARCH" /d:sonar.host.url="http://localhost:8082" /d:sonar.login="cba0e917575a8d2f8b399b1505b7cc5d7f8af36c" /d:sonar.cs.opencover.reportsPaths="tests/*/coverage.opencover.xml" /d:sonar.coverage.exclusions="**Tests*.cs,**/Startup.cs,**/Program.cs,,**/NativeInjectorBootStrapper.cs,**/Migrations/**"
dotnet build ./DotNetSearch.sln
dotnet sonarscanner end /d:sonar.login="cba0e917575a8d2f8b399b1505b7cc5d7f8af36c"
read