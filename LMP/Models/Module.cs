﻿using System.Windows.Input;

namespace LMP.Models
{
    public class Module
    {
        public string Icon { get; set; }

        public string Title { get; set; }

        public ICommand LoadModuleCommand { get; set; }
    }
}
