using System;
using System.Collections.Generic;

namespace RedisCacheDemo.Models;

/// <summary>
/// Unit of measure lookup table.
/// </summary>
public partial class UnitMeasure
{
    /// <summary>
    /// Primary key.
    /// </summary>
    public string UnitMeasureCode { get; set; } = null!;

    /// <summary>
    /// Unit of measure description.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<BillOfMaterial> BillOfMaterials { get; set; } = new List<BillOfMaterial>();

    public virtual ICollection<Product1> Product1SizeUnitMeasureCodeNavigations { get; set; } = new List<Product1>();

    public virtual ICollection<Product1> Product1WeightUnitMeasureCodeNavigations { get; set; } = new List<Product1>();

    public virtual ICollection<ProductVendor> ProductVendors { get; set; } = new List<ProductVendor>();
}
