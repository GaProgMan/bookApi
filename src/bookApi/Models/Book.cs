using System;

namespace bookApi.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        
        /// <summary>
        /// Represents the title of the book
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Represents the author name for the book instance
        /// </summary>
        public string Author { get; set; }
    }
}