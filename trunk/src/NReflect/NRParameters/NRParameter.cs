// NReflect - Easy assembly reflection
// Copyright (C) 2010-2011 Malte Ried
//
// This file is part of NReflect.
//
// NReflect is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// NReflect is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with NReflect. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using NReflect.Modifier;
using NReflect.NRAttributes;

namespace NReflect.NRParameters
{
  /// <summary>
  /// Represents a parameter which is reflected by NReflect.
  /// </summary>
  [Serializable]
  public class NRParameter : IVisitable, IAttributable
  {
    // ========================================================================
    // Con- / Destruction

    #region === Con- / Destruction

    /// <summary>
    /// Initializes a new instance of <see cref="NRParameter"/>.
    /// </summary>
    public NRParameter()
    {
      Attributes = new List<NRAttribute>();
    }

    #endregion

    // ========================================================================
    // Properties

    #region === Properties

    /// <summary>
    /// Gets or sets the name of the parameter.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the type of the parameter.
    /// </summary>
    public NRType Type { get; set; }

    /// <summary>
    /// Gets or sets the full name of the type.
    /// </summary>
    public string TypeFullName { get; set; }

    /// <summary>
    /// Gets or sets the parameter modifier for this parameter.
    /// </summary>
    public ParameterModifier ParameterModifier { get; set; }

    /// <summary>
    /// Gets or sets the default value of the parameter. The value is only valid if
    /// <see cref="ParameterModifier"/> is set to <see cref="Modifier.ParameterModifier.Optional"/>.
    /// </summary>
    public string DefaultValue { get; set; }

    /// <summary>
    /// Gets a list of attributes of the parameter.
    /// </summary>
    public List<NRAttribute> Attributes { get; private set; }

    #endregion

    // ========================================================================
    // Methods

    #region === Methods

    /// <summary>
    /// Accept an <see cref="IVisitor"/> instance on the implementing class and all its children.
    /// </summary>
    /// <param name="visitor">The <see cref="IVisitor"/> instance to accept.</param>
    public void Accept(IVisitor visitor)
    {
      visitor.Visit(this);
    }

    #endregion
  }
}