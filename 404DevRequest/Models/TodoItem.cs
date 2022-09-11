using Canducci.MongoDB.Repository.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _404DevRequest.Models
{
    [BsonCollectionName("todoList")]
    public class TodoItem
    {
        [BsonRequired()]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        
        [Required]
        [Display(Name = "Task title")]
        [StringLength(100)]
        [BsonRequired()]
        [BsonElement("TaskTitle")]
        public string TaskTitle { get; set; }

        [BsonRequired()]
        [BsonElement("CreatedAt")]
        public string CreatedAt { get; set; }

        [Required]
        [Display(Name = "Task description")]
        [StringLength(350, MinimumLength = 50)]
        [BsonRequired()]
        [BsonElement("Description")]
        public string Description { get; set; }
        [BsonRequired()]
        [BsonElement("Priority")]
        public int Priority { get; set; }
    }
}


