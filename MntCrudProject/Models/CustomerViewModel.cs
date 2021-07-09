﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MntCrudProject.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public String Email { get; set; }

        public String Address { get; set; }

        public String Phone { get; set; }

    }
}