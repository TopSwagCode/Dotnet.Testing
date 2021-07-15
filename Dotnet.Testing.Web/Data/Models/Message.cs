﻿using System.ComponentModel.DataAnnotations;

namespace Dotnet.Testing.Web.Data.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(200, ErrorMessage = "There's a 200 character limit on messages. Please shorten your message.")]
        public string Text { get; set; }
    }
}