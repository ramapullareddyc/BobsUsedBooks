using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Domain.ReferenceData
{
    [Table("ReferenceData", Schema = "dbo")]
    public class ReferenceDataItem : Entity
    {
        // An empty constructor is required by EF Core
        private ReferenceDataItem() { }

        public ReferenceDataItem(ReferenceDataType referenceDataType, string text)
        {
            DataType = referenceDataType;
            Text = text;
        }

        public ReferenceDataType DataType { get; set; }

        public string Text { get; set; }
    }
}
