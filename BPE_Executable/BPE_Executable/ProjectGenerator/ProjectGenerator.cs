using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BukkitPluginEditor.GUI.FileWizardPanels;

namespace BukkitPluginEditor.ProjectGenerator
{
    
    /// <summary>
    /// Generates a project and performs necessary I/O operations to create the project.
    /// </summary>
    /// <remarks>This class is NOT thread-safe.</remarks>
    public class ProjectGenerator
    {

        protected string ProjectName;
        protected string ProjectPath;
        protected List<string> Dependencies;
        protected ProjectTypes ProjectType;

        /// <summary>
        /// Defines the project types that can be generated automatically.
        /// </summary>
        public enum ProjectTypes
        {
            /// <summary>
            /// ProjectType for Eclipse projects.
            /// </summary>
            Eclipse, 

            /// <summary>
            /// ProjectType for IntelliJ modules.
            /// </summary>
            IntelliJ, 

            /// <summary>
            /// ProjectType for Maven projects.
            /// </summary>
            Maven
        }

        /// <summary>
        /// Constructs a Project Generator object.
        /// </summary>
        /// <param name="ProjectName"></param>
        /// <param name="ProjectPath"></param>
        /// <param name="Dependencies"></param>
        /// <param name="ProjectType"></param>
        public ProjectGenerator(string ProjectName, string ProjectPath, List<string> Dependencies, ProjectTypes ProjectType)
        {
            this.ProjectName = ProjectName;
            this.ProjectPath = ProjectPath;
            this.Dependencies = Dependencies;
            this.ProjectType = ProjectType;
        }

        /// <summary>
        /// Creates the project with the specified project type.
        /// </summary>
        /// <returns>True on success.</returns>
        public bool CreateProject()
        {
            switch (ProjectType)
            {
                case ProjectTypes.Eclipse:
                    return CreateEclipseProject();
                case ProjectTypes.IntelliJ:
                    return CreateIntelliJModule();
                case ProjectTypes.Maven:
                    return CreateMavenProject();

                default:
                    return false;
            }
        }

        /// <summary>
        /// Creates an Eclipse project.
        /// </summary>
        /// <returns></returns>
        protected bool CreateEclipseProject()
        {
            return true;
        }

        /// <summary>
        /// Creates an IntelliJ Module.
        /// </summary>
        /// <returns></returns>
        protected bool CreateIntelliJModule()
        {
            return true;
        }

        /// <summary>
        /// Creates an Eclipse-style Maven project.
        /// </summary>
        /// <returns></returns>
        protected bool CreateMavenProject()
        {
            return true;
        }

    }
}
