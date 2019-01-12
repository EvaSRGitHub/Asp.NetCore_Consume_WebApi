using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiModels
{
    public class ToDoInputModel
    {
        public string JsonObj { get; set; }

        public int Id { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "IsComplete")]
        [Display(Name = "Completed")]
        public bool IsComplete { get; set; }

        public IEnumerable<ToDoViewModel> Items { get; set; } = new HashSet<ToDoViewModel>();
    }
}
