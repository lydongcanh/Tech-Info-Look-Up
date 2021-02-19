﻿using System;

namespace TechInfoLookUp.Data.Entities
{
    public class Tag : IIdObject<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
