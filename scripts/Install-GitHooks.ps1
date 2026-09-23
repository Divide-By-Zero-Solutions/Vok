$ErrorActionPreference = 'Stop'
$repositoryRoot = Split-Path -Parent $PSScriptRoot

if (-not (Test-Path (Join-Path $repositoryRoot '.git'))) {
	throw 'Git is not initialized in this workspace. Run git init first.'
}

if (-not (Get-Command git -ErrorAction SilentlyContinue)) {
	throw 'Git is not available on PATH.'
}

git -C $repositoryRoot config core.hooksPath .githooks
Write-Host 'Configured Git to use .githooks.'
