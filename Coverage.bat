@ECHO OFF

dotnet test MyRestaurant.Services.Tests/MyRestaurant.Services.Tests.csproj ^
			-c Release ^
			/p:CollectCoverage=true ^
			/p:CoverletOutput=%CD%/CoverageReports/Services.xml ^
			/p:CoverletOutputFormat=opencover ^
			/p:Exclude="[xunit.*]"

dotnet test myrestaurant.seeddata.tests/myrestaurant.seeddata.tests.csproj ^
			-c release ^
			/p:collectcoverage=true ^
			/p:coverletoutput=%cd%/coveragereports/seeddata.xml ^
			/p:coverletoutputformat=opencover ^
			/p:exclude="[xunit.*]"

dotnet test MyRestaurant.Business.Tests/MyRestaurant.Business.Tests.csproj ^
			-c Release ^
			/p:CollectCoverage=true ^
			/p:CoverletOutput=%CD%/CoverageReports/Business.xml ^
			/p:CoverletOutputFormat=opencover ^
			/p:Exclude="[xunit.*]"

dotnet test MyRestaurant.Api.Tests/MyRestaurant.Api.Tests.csproj ^
			-c Release ^
			/p:CollectCoverage=true ^
			/p:CoverletOutput=%CD%/CoverageReports/Api.xml ^
			/p:CoverletOutputFormat=opencover ^
			/p:Exclude="[xunit.*]"

%UserProfile%\.nuget\packages\reportgenerator\4.8.5\tools\net5.0\ReportGenerator.exe ^
			"-reports:%CD%\CoverageReports\*.xml" ^
			"-targetdir:CoverageReports" -reporttypes:HTML

start %CD%\CoverageReports\index.html