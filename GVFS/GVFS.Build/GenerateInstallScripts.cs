﻿using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.IO;
using System.Text;

namespace GVFS.PreBuild
{
    public class GenerateInstallScripts : Task
    {
        [Required]
        public string GitInstallerFilename { get; set; }

        [Required]
        public string VFSForGitSetupFilename { get; set; }

        [Required]
        public string GitInstallBatPath { get; set; }

        [Required]
        public string VFSForGitInstallBatPath { get; set; }

        [Required]
        public string UnifiedInstallBatPath { get; set; }

        public override bool Execute()
        {
            this.Log.LogMessage(MessageImportance.High, "Creating install scripts for " + this.GitInstallerFilename);

            File.WriteAllText(
                this.GitInstallBatPath,
                this.GetGitInstallCommand());

            File.WriteAllText(
                this.VFSForGitInstallBatPath,
                this.GetVFSForGitInstallCommand());

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("REM AUTOGENERATED DO NOT EDIT");
            sb.AppendLine();
            sb.AppendLine(this.GetGitInstallCommand());
            sb.AppendLine();
            sb.AppendLine(this.GetVFSForGitInstallCommand());

            File.WriteAllText(
                this.UnifiedInstallBatPath,
                sb.ToString());

            return true;
        }

        public string GetGitInstallCommand()
        {
            return "%~dp0\\" + this.GitInstallerFilename + @" /DIR=""C:\Program Files\Git"" /NOICONS /COMPONENTS=""ext,ext\shellhere,ext\guihere,assoc,assoc_sh"" /GROUP=""Git"" /VERYSILENT /SUPPRESSMSGBOXES /NORESTART /ALLOWDOWNGRADE=1";
        }

        public string GetVFSForGitInstallCommand()
        {
            return "%~dp0\\" + this.VFSForGitSetupFilename + " /VERYSILENT /SUPPRESSMSGBOXES /NORESTART";
        }
    }
}
