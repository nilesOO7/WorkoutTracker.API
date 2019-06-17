"..\..\..\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe" ^
	-target:"..\..\..\packages\NUnit.ConsoleRunner.3.8.0\tools\nunit3-console.exe" ^
	-targetargs:"WorkoutTracker.Tests.dll" ^
	-filter:"+[WorkoutTracker.Service]* +[WorkoutTracker.Data]*" ^
	-excludebyattribute:"System.CodeDom.Compiler.GeneratedCodeAttribute" ^
	-register:user ^
	-output:"_CodeCoverageResult.xml"

"..\..\..\packages\ReportGenerator.3.1.2\tools\ReportGenerator.exe" ^
	-reports:"_CodeCoverageResult.xml" ^
	-targetdir:"_CodeCoverageReport"
