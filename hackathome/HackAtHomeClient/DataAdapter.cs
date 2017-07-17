﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HackAtHome.CustomAdapters;
using HackAtHome.Entities;

namespace HackAtHomeClient
{
    class DataAdapter : Fragment
    {
        public List<Evidence> list { get; set; }
        public int position { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RetainInstance = true;
        }
    }
}