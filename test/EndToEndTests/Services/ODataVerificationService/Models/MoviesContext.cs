﻿//---------------------------------------------------------------------
// <copyright file="MoviesContext.cs" company=".NET Foundation">
//      Copyright (c) .NET Foundation and Contributors. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODataVerificationService.Models
{
    public class MoviesContext
    {
        static List<Movie> _movies;
        static MoviesContext()
        {
            _movies = new List<Movie>()
            {
                new Movie() { ID=1, Title = "Maximum Payback", Year = 1990 },
                new Movie() { ID=2, Title = "Inferno of Retribution", Year = 2005 },
                new Movie() { ID=3, Title = "Fatal Vengeance 2", Year = 2012 },
                new Movie() { ID=4, Title = "Sudden Danger", Year = 2012 },
                new Movie() { ID=5, Title = "Beyond Outrage", Year = 2014 },
                new Movie() { ID=6, Title = "The Nut Job", Year = 2014 }
            };
        }

        public List<Movie> Movies { get { return _movies; } }
    }
}
