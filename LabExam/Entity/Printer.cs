using System;
using System.IO;
using LabExam.Entity;

namespace LabExam.Entity
{
    /// <summary>
    /// Class describing printer in general
    /// </summary>
    public sealed class Printer : BasePrinter, IEquatable<Printer>
    {
        public Printer(string name, string model)
        {
            this.Name = name;

            this.Model = model;
        }

        /// <summary>
        /// Method for all inheritor
        /// </summary>
        /// <param name="stream">stream for print</param>
        protected override void CurrentPrint(Stream stream)
        {
            for (int i = 0; i < stream.Length; i++)
            {
                // simulate printing
                Console.WriteLine(stream.ReadByte());
            }
        }

        /// <summary>
        /// Equality two printers
        /// </summary>
        /// <param name="other">instance printer for equality</param>
        /// <returns>true if printers is equals</returns>
        public bool Equals(Printer other)
        {
            if (ReferenceEquals(null, other)) return false;

            if (ReferenceEquals(this, other)) return true;

            if (!(this.Name == other.Name && this.Model == other.Model)) return false;

            return true;
        }

        /// <summary>
        /// Override method Equals inheritans object
        /// </summary>
        /// <param name="obj">object for equatable</param>
        /// <returns>true if objects is equal</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != this.GetType()) return false;

            return Equals((Printer) obj);
        }

        /// <summary>
        /// Override GetHashCode if override method Equals
        /// </summary>
        /// <returns>hash code printer</returns>
        public override int GetHashCode()
        {
            return this.Name.GetHashCode() + this.Model.GetHashCode();
        }
    }
}