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
    public partial class SysLog:IEntity
    {
        public int Id { get; set; }
        public string LogContent { get; set; }
        public string LogIp { get; set; }
        public int LogType { get; set; }
        public string LogUrl { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public System.DateTime UpdateTime { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsDeleted { get; set; }
    }
}
