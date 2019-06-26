﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.ComponentModel.Composition;
using System.Windows.Controls;

using Microsoft.VisualStudio.ProjectSystem.Debug;

#nullable disable

namespace Microsoft.VisualStudio.ProjectSystem.VS.PropertyPages
{
    /// <summary>
    /// Implementation of ILaunchSettingsUIProvider for the Executable launch type.
    /// </summary>
    [Export(typeof(ILaunchSettingsUIProvider))]
    [AppliesTo(ProjectCapability.LaunchProfiles)]
    [Order(Order.Lowest)]              // Lowest priority to allow this to be overridden
    internal class ExecutableLaunchSettingsUIProvider : ILaunchSettingsUIProvider
    {
        /// <summary>
        /// Required to control the MEF scope
        /// </summary>
        [ImportingConstructor]
        public ExecutableLaunchSettingsUIProvider(UnconfiguredProject uncProject)
        {
        }

        /// <summary>
        /// The name of the command that is written to the launchSettings.json file
        /// </summary>
        public string CommandName
        {
            get
            {
                return LaunchSettingsProvider.RunExecutableCommandName;
            }
        }

        /// <summary>
        /// The name to display in the dropdown for this command
        /// </summary>
        public string FriendlyName
        {
            get
            {
                return PropertyPageResources.ProfileKindExecutableName;
            }
        }

        /// <summary>
        /// Launch url is not supported
        /// </summary>
        public bool ShouldEnableProperty(string propertyName)
        {
            return string.Equals(propertyName, UIProfilePropertyName.LaunchUrl, System.StringComparison.OrdinalIgnoreCase) ? false : true;
        }

        /// <summary>
        /// No custom UI
        /// </summary>
        public UserControl CustomUI { get { return null; } }

        /// <summary>
        /// Called when the selected profile changes to a profile which matches this command. curSettings will contain 
        /// the current values from the page, and activeProfile will point to the active one.
        /// </summary>
        public void ProfileSelected(IWritableLaunchSettings curSettings)
        {
            return;
        }
    }
}
