﻿using ReactiveUI;

namespace Samples.Wpf.ViewModels
{
    public class PersonViewModel
    {
        public string Name { get; set; }
        public ReactiveList<HouseViewModel> OwnedHouses { get; set; }
        public decimal Age { get; set; }
    }
}