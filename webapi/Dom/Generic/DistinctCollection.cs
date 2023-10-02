using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Dom.Generic
{
    [Owned]
    public class DistinctCollection<T>
    {
        public DistinctCollection() {
            this.Csv = "";
        }

        public string Csv { get; set; }

        [NotMapped]
        public IEnumerable<T> Data { 
            get => !string.IsNullOrEmpty(Csv) ? Csv.Split(",").Select(obj => JsonConvert.DeserializeObject<T>(obj)) : new T[0]; 
            private set => this.Csv = value.Count() != 0 && value != null ?  string.Join(",", value) : ""; 
        }

        public void Add(T value)
        {
            if(Data.Contains(value)) throw new InvalidOperationException("Element is already in Collection");
            var newlist = this.Data.ToList();
            newlist.Add(value);
            this.Data = newlist;
        }

        public void Remove(T value)
        {
            var newlist = this.Data.ToList();
            newlist.Remove(value);
            this.Data = newlist;
        }
    }

}
