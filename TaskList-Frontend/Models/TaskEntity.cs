using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaskList_Frontend.Models
{
    public class TaskEntity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("taskTitle")]
        public string TaskTitle { get; set; }
        [JsonPropertyName("taskDescription")]
        public string TaskDescription { get; set; }
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("doneAt")]
        public DateTime? DoneAt { get; set; }
        [JsonPropertyName("status")]
        public int Status { get; set; }


        [JsonIgnore]
        public string StatusText => Status switch
        {
            0 => "Pendente",
            1 => "Em Progresso",
            2 => "Concluído",
            _ => "-"
        };

        public string CreatedAtFormatted => CreatedAt.ToString("dd/MM/yyyy HH:mm");
        public string DoneAtFormatted => DoneAt?.ToString("dd/MM/yyyy HH:mm") ?? "-";

    }
}
