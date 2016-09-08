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

using System.Collections.Generic;
using NReflect.NREntities;

namespace NReflect
{
  /// <summary>
  /// Classes implementing this interface can contain entities like classe, interfaces, ...
  /// </summary>
  public interface IEntityContainer
  {
    /// <summary>
    /// Gets a list of reflected classes.
    /// </summary>
    List<NRClass> Classes { get; }

    /// <summary>
    /// Gets a list of reflected interfaces.
    /// </summary>
    List<NRInterface> Interfaces { get; }

    /// <summary>
    /// Gets a list of reflected structs.
    /// </summary>
    List<NRStruct> Structs { get; }

    /// <summary>
    /// Gets a list of reflected enums.
    /// </summary>
    List<NREnum> Enums { get; }

    /// <summary>
    /// Gets a list of reflected delegates.
    /// </summary>
    List<NRDelegate> Delegates { get; }
  }
}