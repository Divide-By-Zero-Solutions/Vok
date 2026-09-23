[CmdletBinding()]
param(
	[string]$Root = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$requiredFiles = @(
	'docs/README.md',
	'docs/architecture.md',
	'docs/data-model.md',
	'docs/diagrams.md',
	'docs/standards.md',
	'docs/testing.md',
	'docs/api.md',
	'docs/performance.md',
	'docs/deployment.md',
	'Vok.Domain/README.md',
	'Vok.Infrastructure/README.md',
	'Vok.Maui/README.md',
	'Vok.Tests/README.md',
	'performance/README.md'
)

foreach ($relativePath in $requiredFiles) {
	$path = Join-Path $Root $relativePath
	if (-not (Test-Path $path -PathType Leaf)) {
		throw "Required documentation file is missing: $relativePath"
	}
}

$markdownFiles = Get-ChildItem -Path $Root -Filter '*.md' -File -Recurse |
	Where-Object { $_.FullName -notmatch '[\\/]bin[\\/]|[\\/]obj[\\/]|[\\/]BenchmarkDotNet.Artifacts[\\/]' }
$mermaidBlocks = 0
foreach ($file in $markdownFiles) {
	$content = Get-Content $file.FullName -Raw
	$mermaidBlocks += ([regex]::Matches($content, '(?m)^```mermaid\s*$')).Count
}
if ($mermaidBlocks -lt 6) {
	throw "Expected at least six Mermaid diagrams, found $mermaidBlocks."
}

$projects = Get-ChildItem -Path $Root -Filter '*.csproj' -File -Recurse |
	Where-Object { $_.FullName -notmatch '[\\/]obj[\\/]' }
foreach ($project in $projects) {
	$content = Get-Content $project.FullName -Raw
	if ($content -notmatch 'GenerateDocumentationFile') {
		$projectDirectory = Get-Item $project.DirectoryName
		$propsFound = $false
		while ($null -ne $projectDirectory) {
			if (Test-Path (Join-Path $projectDirectory.FullName 'Directory.Build.props')) {
				$propsFound = $true
				break
			}
			$projectDirectory = $projectDirectory.Parent
		}
		if (-not $propsFound) {
			throw "XML documentation generation is not configured for $($project.FullName)"
		}
	}
}

Write-Host "Documentation validation passed: $($requiredFiles.Count) required files, $mermaidBlocks Mermaid diagrams, $($projects.Count) projects checked."
