using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace WebApiModels
{
    [JsonObject]
    public class ToDoViewModel
    {
        [JsonProperty(PropertyName = "Id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "IsComplete")]
        [Display(Name = "Completed")]
        public bool IsComplete { get; set; }

        
    }
}
