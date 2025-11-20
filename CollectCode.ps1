# --- SETTINGS ---

# Get the folder where this script is located
$ScriptDir = $PSScriptRoot

# Target the "InternshipNet" folder inside the current folder
$SourcePath = Join-Path -Path $ScriptDir -ChildPath "InternshipNet"

# Output file path
$OutputFile = Join-Path -Path $ScriptDir -ChildPath "FullCodeProject.txt"

# Extensions to look for
$Extensions = @(".cs", ".xaml", ".xaml.cs", ".json", ".config", ".js", ".html", ".css")

# --- LOGIC ---

Clear-Host
Write-Host "Script started." -ForegroundColor Cyan
Write-Host "Looking for code in: $SourcePath" -ForegroundColor Gray

# Check if the target folder exists
if (-not (Test-Path $SourcePath)) {
    Write-Host "`r`nERROR: Folder 'InternshipNet' not found inside this folder!" -ForegroundColor Red
    Write-Host "Make sure the folder structure is correct." -ForegroundColor Red
    Read-Host "Press Enter to exit..."
    exit
}

# Remove old output file if exists
if (Test-Path $OutputFile) {
    Remove-Item $OutputFile
}

try {
    # Find files
    $Files = Get-ChildItem -Path $SourcePath -Recurse -File -ErrorAction Stop | Where-Object {
        # 1. Check extension
        $isExtensionValid = $Extensions -contains $_.Extension

        # 2. Ignore bin and obj folders
        $pathStr = $_.FullName
        $isNotBinObj = ($pathStr -notmatch "\\bin\\") -and ($pathStr -notmatch "\\obj\\")

        return $isExtensionValid -and $isNotBinObj
    }

    if ($Files.Count -eq 0) {
        Write-Host "`r`nNo code files found!" -ForegroundColor Yellow
    }
    else {
        Write-Host "`r`nFound files: $($Files.Count). Processing..." -ForegroundColor Green
        
        foreach ($file in $Files) {
            Write-Host " -> Reading: $($file.Name)" -ForegroundColor DarkGray
            
            # Create a header
            $Header = "`r`n" + ("=" * 80) + "`r`n" + 
                      "FILE: $($file.FullName)" + "`r`n" + 
                      ("=" * 80) + "`r`n"

            # Write header and content to output file
            Add-Content -Path $OutputFile -Value $Header -Encoding UTF8
            $Content = Get-Content -Path $file.FullName
            Add-Content -Path $OutputFile -Value $Content -Encoding UTF8
        }
        
        Write-Host "`r`nSUCCESS! All code saved to:" -ForegroundColor Green
        Write-Host $OutputFile -ForegroundColor White
    }
}
catch {
    Write-Host "`r`nCRITICAL ERROR: $_" -ForegroundColor Red
}

# Keep window open
Write-Host "`r`nPress Enter to close..."
Read-Host