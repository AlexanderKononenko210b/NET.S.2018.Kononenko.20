﻿// It is instance but not type!
//Make abstract class where define property Name and Model and Method Print as Required for all inheritens


//using System;
//using System.IO;

//namespace LabExam
//{
//    internal class CanonPrinter
//    {
//        public CanonPrinter()
//        {
//            Name = "Canon";
//            Model = "123x";
//        }

//        public void Print(FileStream fs)
//        {
//            for (int i = 0; i < fs.Length; i++)
//            {
//                // simulate printing
//                Console.WriteLine(fs.ReadByte());
//            }
//        }

//        public string Name { get; set; }

//        public string Model { get; set; }
//    }
//}