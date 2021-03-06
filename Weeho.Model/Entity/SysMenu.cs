//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Weeho.Model.Entity
{
    using System;
    using System.Collections.Generic;
    
    
    using Weeho.Model;
    public partial class SysMenu:IEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysMenu()
        {
            this.SysMenu1 = new HashSet<SysMenu>();
            this.SysRoleMenu = new HashSet<SysRoleMenu>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Url { get; set; }
        public Nullable<int> ParentId { get; set; }
        public bool IsShow { get; set; }
        public int SortId { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public System.DateTime UpdateTime { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysMenu> SysMenu1 { get; set; }
        public virtual SysMenu SysMenu2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysRoleMenu> SysRoleMenu { get; set; }
    }
}
