
[CmdletBinding()]
param(
    $RootDrive = "E"
)


$apps = import-csv -Path .\ApplicationList.csv
$boxes = import-csv -Path .\DirectoryList.csv
[string[]]$paths = @()
$basePath = $RootDrive + ":\BizTalk\"
$paths += $basePath

foreach($a in $apps) {

	$appPath = $basePath + $a.Application.ToString() + "\"
	$paths += $appPath
	$paths += $appPath + "Inboxes\"
	$paths += $appPath + "Outboxes\"
	$paths += $appPath + "Archives\"
	$paths += $appPath + "Archives\Inboxes\"
	$paths += $appPath + "Archives\Outboxes\"

	foreach($b in $boxes)
	{
		if($b.Application.ToString() -eq $a.Application.ToString())
		{
			#$paths += Create-BoxFolders($basePath, $b.Application.ToString(), $b.Type.ToString(), $b.Archive.ToString(), $b.Name.ToString(), $b.SubPath.ToString())

			if($b.Type.ToString() -eq "SendPort")
			{
				if($b.Archive.ToString() -eq "No")
				{
					$paths += $basePath + $a.Application.ToString() + "\" + "Outboxes" + "\" + $b.Name.ToString()
				}
				elseif($b.Archive.ToString() -eq "Yes")
				{
					$paths += $basePath + $a.Application.ToString() + "\" + "Archives" + "\" + "Outboxes" + "\" + $b.Name.ToString()
				}
			}
			elseif($b.Type.ToString() -eq "ReceiveLocation")
			{
				if($b.Archive.ToString() -eq "No")
				{
					$paths += $basePath + $a.Application.ToString() + "\" + "Inboxes" + "\" + $b.Name.ToString();
				}
				elseif($b.Archive.ToString() -eq "Yes")
				{
					$paths += $basePath + $a.Application.ToString() + "\" + "Archives" + "\" + "Inboxes" + "\" + $b.Name.ToString()
				}
			}
		}	
	}
}

foreach($p in $paths)
{
	$check = Test-Path -Path $p.ToString()
	if($check -eq $false)
	{
		New-Item -Path $p.ToString() -Type Directory
		#$p.ToString();
	}
}