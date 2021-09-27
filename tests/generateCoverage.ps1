$ResultsFolder = 'CoverageResults';
if (Test-Path -Path $ResultsFolder) {
    Remove-Item $ResultsFolder -Recurse
}

dotnet test NintendoGameStore.UnitTests\NintendoGameStore.UnitTests.csproj /p:CollectCoverage=true /p:Exclude="[*.Migrations]*%2c[*Tests]*" /p:CoverletOutput=../$ResultsFolder/
dotnet test NintendoGameStore.IntegrationTests\NintendoGameStore.IntegrationTests.csproj /p:CollectCoverage=true /p:Exclude="[*.Migrations]*%2c[*Tests]*" /p:CoverletOutput=../$ResultsFolder/ /p:MergeWith="../$ResultsFolder/coverage.json" /p:CoverletOutputFormat="cobertura"
cd $ResultsFolder
reportgenerator "-reports:coverage.cobertura.xml" "-targetdir:coveragereport" -reporttypes:Html