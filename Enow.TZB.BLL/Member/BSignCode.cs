using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enow.TZB.BLL
{
   public class BSignCode
    {
   /// <summary>
   /// 新增签名记录
   /// </summary>
   /// <param name="SignCode">签名码</param>
   /// <returns>Guid</returns>
       public static string AddSignCode(string SignCode)
       {
           using (FWDC rdc=new FWDC())
           {
               tbl_SignCode model = new tbl_SignCode();
               model.Id = Guid.NewGuid().ToString();
               model.CodeSign = SignCode;
               rdc.tbl_SignCode.InsertOnSubmit(model);
               rdc.SubmitChanges();
               return model.Id;
           }
       }

       /// <summary>
       /// 根据签名获取ID
       /// </summary>
       /// <param name="Id">Guid</param>
       /// <returns>SignCode</returns>
       public static string GetCodeId(string Id)
       {
           string rv = "";
           using (FWDC rdc=new FWDC())
           {
               var model = rdc.tbl_SignCode.FirstOrDefault(n => n.Id == Id);
               if (model != null)
               {
                   rv = model.CodeSign;
               }
               else
               {
                   rv = "";
               }
               return rv;
           }
       }
    }
}
