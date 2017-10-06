// Copyright © 2004, 2010, Oracle and/or its affiliates. All rights reserved.
//
// MySQL Connector/NET is licensed under the terms of the GPLv2
// <http://www.gnu.org/licenses/old-licenses/gpl-2.0.html>, like most 
// MySQL Connectors. There are special exceptions to the terms and 
// conditions of the GPLv2 as it is applied to this software, see the 
// FLOSS License Exception
// <http://www.mysql.com/about/legal/licensing/foss-exception.html>.
//
// This program is free software; you can redistribute it and/or modify 
// it under the terms of the GNU General Public License as published 
// by the Free Software Foundation; version 2 of the License.
//
// This program is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
// or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License 
// for more details.
//
// You should have received a copy of the GNU General Public License along 
// with this program; if not, write to the Free Software Foundation, Inc., 
// 51 Franklin St, Fifth Floor, Boston, MA 02110-1301  USA

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace MySqlProvider
{
  [Serializable]
  public sealed class MySqlProviderPermission : DBDataPermission
  {

  #region Contructors

  public MySqlProviderPermission(PermissionState permissionState)
    : base(permissionState)
  {
  }

  private MySqlProviderPermission(MySqlProviderPermission permission):base(permission)
  { 
  }

  internal MySqlProviderPermission(MySqlProviderPermissionAttribute permissionAttribute):base(permissionAttribute)
  { 
  }

	internal MySqlProviderPermission (DBDataPermission permission)
		: base (permission)
	{
	}

  internal MySqlProviderPermission(string connectionString)
    : base(PermissionState.None)
  {
    if ((connectionString == null) || connectionString.Length == 0)
      base.Add(string.Empty, string.Empty, KeyRestrictionBehavior.AllowOnly);
    else
      base.Add(connectionString, string.Empty, KeyRestrictionBehavior.AllowOnly);
  }


  #endregion
   
  #region Methods
  
  /// <summary>
  /// Adds a new connection string with set of restricted keywords to the MySqlProviderPermission object 
  /// </summary>
  ///<param name="connectionString">Settings to be used for the connection</param>
  ///<param name="restrictions">Keywords to define the restrictions</param>
  ///<param name="behavior">KeyRestrictionBehavior to be used</param>
  public override void Add(string connectionString, string restrictions, KeyRestrictionBehavior behavior)
  {
    base.Add(connectionString, restrictions, behavior);
  }
  
  /// <summary>
  /// Returns MySqlProviderPermission as an IPermission
  /// </summary>
  /// <returns></returns>
  public override IPermission Copy()
  {
    return new MySqlProviderPermission(this);
  }

  #endregion

  }
}
