using System.ComponentModel.DataAnnotations;

namespace CommandService.Dtos
{
    public class CommandReadDTO
    {
        [Required]
        public string HowTo { get; set; }

        [Required]
        public string CommandLine { get; set; }
    }
}