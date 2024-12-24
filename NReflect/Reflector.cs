// NReflect - Easy assembly reflection
// Copyright (C) 2010-2013 Malte Ried
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
using System.IO;
using System.Reflection;
using System.Text;
using NReflect.Filter;

namespace NReflect
{
    /// <summary>
    /// An instance of this class is able to reflect the types of an assembly.
    /// </summary>
    /// <remarks>
    /// If reflection takes place inside the same app domain it is called from, the
    /// reflected assembly files stay opened until this app domain is unloaded. If
    /// the current app domain is the starting app domain, this can only be done via
    /// closing the application.
    /// </remarks>
    public class Reflector : MarshalByRefObject
    {
        // ========================================================================
        // Properties

        #region === Properties

        /// <summary>
        /// Gets or sets the type filter used to determine which types to reflect.
        /// </summary>
        private IFilter Filter { get; set; }

        #endregion

        // ========================================================================
        // Methods

        #region === Methods

        /// <summary>
        /// Reflects the types of an assembly.
        /// </summary>
        /// <param name="fileName">The file name of the assembly to reflect.</param>
        /// <param name="filter">The type filter used to determine which types to reflect.</param>
        /// <returns>The result of the reflection.</returns>
        public NRAssembly Reflect(string fileName, ref IFilter filter)
        {
            Filter = filter;
            return Reflect(fileName);
        }

        /// <summary>
        /// Reflects the types of the provided assembly.
        /// </summary>
        /// <param name="assembly">The assembly to reflect.</param>
        /// <param name="filter">The type filter used to determine which types to reflect.</param>
        /// <returns>The result of the reflection.</returns>
        public NRAssembly Reflect(Assembly assembly, ref IFilter filter)
        {
            Filter = filter;
            ReflectionWorker reflectionWorker = new ReflectionWorker
            {
                Filter = Filter ?? new ReflectAllFilter()
            };

            return reflectionWorker.Reflect(assembly);
        }

        /// <summary>
        /// Reflects the types of an assembly.
        /// </summary>
        /// <param name="fileName">The file name of the assembly to reflect.</param>
        /// <returns>The result of the reflection.</returns>
        private NRAssembly Reflect(string fileName)
        {
            ReflectionWorker reflectionWorker = new ReflectionWorker
            {
                Filter = Filter ?? new ReflectAllFilter()
            };

            NRAssembly nrAssembly = null;
            try
            {
                nrAssembly = reflectionWorker.Reflect(fileName);
            }
            catch (ReflectionTypeLoadException ex)
            {
                var sb = new StringBuilder();
                foreach (var exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    if (exSub is FileNotFoundException exFileNotFound)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }

                    sb.AppendLine();
                }

                var errorMessage = sb.ToString();
                //Display or log the error based on your application.
            }

            return nrAssembly;
        }

        #endregion
    }
}