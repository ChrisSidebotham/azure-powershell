// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Commands.Sql.Common;
using Microsoft.Azure.Commands.Sql.InstanceActiveDirectoryAdministrator.Model;
using Microsoft.Azure.Commands.Sql.InstanceActiveDirectoryAdministrator.Services;
using Microsoft.Azure.Commands.Sql.ManagedInstance.Model;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Sql.InstanceActiveDirectoryAdministrator.Cmdlet
{
    public abstract class AzureSqlInstanceActiveDirectoryAdministratorCmdletBase : AzureSqlCmdletBase<IEnumerable<AzureSqlInstanceActiveDirectoryAdministratorModel>, AzureSqlInstanceActiveDirectoryAdministratorAdapter>
    {
		protected const string UseInputObjectParameterSet = "UseInputObjectParameterSet";
		protected const string UseResourceGroupAndInstanceNameParameterSet = "UseResourceGroupAndInstanceNameParameterSet";

		/// <summary>
		/// Server resource
		/// </summary>
		[Parameter(ParameterSetName = UseInputObjectParameterSet,
			Mandatory = false,
			ValueFromPipeline = true,
			HelpMessage = "The managed instance object to use.")]
		[ValidateNotNullOrEmpty]
		public AzureSqlManagedInstanceModel InputObject { get; set; }

		/// <summary>
		/// Gets or sets the name of the resource group to use.
		/// </summary>
		[Parameter(ParameterSetName = UseResourceGroupAndInstanceNameParameterSet,
			Mandatory = true,
			ValueFromPipelineByPropertyName = true,
			Position = 0,
			HelpMessage = "The name of the resource group.")]
		[ResourceGroupCompleter]
		[ValidateNotNullOrEmpty]
		public override string ResourceGroupName { get; set; }

		/// <summary>
		/// Gets or sets the name of the managed instance to use.
		/// </summary>
		[Parameter(ParameterSetName = UseResourceGroupAndInstanceNameParameterSet,
			Mandatory = true,
			ValueFromPipelineByPropertyName = true,
			Position = 1,
			HelpMessage = "SQL Managed Instance name.")]
		[ResourceNameCompleter("Microsoft.Sql/managedInstances", "ResourceGroupName")]
		[ValidateNotNullOrEmpty]
		public string InstanceName { get; set; }

		protected string GetResourceGroupName()
		{
			if (string.Equals(this.ParameterSetName, UseInputObjectParameterSet, StringComparison.OrdinalIgnoreCase))
			{
				return InputObject.ResourceGroupName;
			}

			return ResourceGroupName;
		}

		protected string GetInstanceName()
		{
			if (string.Equals(this.ParameterSetName, UseInputObjectParameterSet, StringComparison.OrdinalIgnoreCase))
			{
				return InputObject.ManagedInstanceName;
			}

			return InstanceName;
		}
		
		/// <summary>
		/// Initializes the adapter
		/// </summary>
		/// <returns></returns>
		protected override AzureSqlInstanceActiveDirectoryAdministratorAdapter InitModelAdapter()
        {
            return new AzureSqlInstanceActiveDirectoryAdministratorAdapter(DefaultProfile.DefaultContext);
        }
    }
}
