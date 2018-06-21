# Run in Package Manager Console with `./download-packages-license.ps1`.
# If access denied, execute `Set-ExecutionPolicy -Scope Process -ExecutionPolicy RemoteSigned`.

Split-Path -parent $dte.Solution.FileName | cd; 
Remove-Item ".\licenses\raw" -Force -Recurse;
New-Item -ItemType Directory -Path ".\licenses\raw";
@( Get-Project -All | ? { $_.ProjectName -and ($_.ProjectName -ne "Hsr.Campus.Core.Test")} | % {
    Get-Package -ProjectName $_.ProjectName | ? { $_.LicenseUrl }
} ) | Sort-Object Id -Unique | % {
    $pkg = $_;
    Try {
        if ($pkg.Id -notlike 'microsoft*' -and $pkg.LicenseUrl.StartsWith('http')) {
            Write-Host ("Download license for package " + $pkg.Id + " from " + $pkg.LicenseUrl);
            #Write-Host (ConvertTo-Json ($pkg));

            $licenseUrl = $pkg.LicenseUrl
            if ($licenseUrl.contains('github.com')) {
                $licenseUrl = $licenseUrl.replace("/blob/", "/raw/")
            }

            (New-Object System.Net.WebClient).DownloadFile($licenseUrl, (Join-Path (pwd) 'licenses\raw\') + $pkg.Id + ".txt");
        }
    }
    Catch [system.exception] {
        Write-Host ("Could not download license for " + $pkg.Id)
    }
}