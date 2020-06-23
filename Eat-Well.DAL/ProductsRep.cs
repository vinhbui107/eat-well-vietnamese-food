using Eat_Well.Common.DAL;
using Eat_Well.Common.Rsp;
using Eat_Well.DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Eat_Well.DAL
{
    public class ProductsRep : GenericRep<EatWellDBContext, Products>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override Products Read(int id)
        {
            var res = All.FirstOrDefault(p => p.ProductId == id);
            return res;
        }


        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affect</returns>
        public int Remove(int id)
        {
            var m = base.All.First(i => i.ProductId == id);
            m = base.Delete(m); //TODO
            return m.ProductId;
        }

        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>

        #endregion
        //=================================================================
        //=================================================================
        
        #region -- Create Product --
        public SingleRsp CreateProduct(Products pro)
        {
            var res = new SingleRsp();
            using (var context = new EatWellDBContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var t = context.Products.Add(pro);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }

        #endregion
        //=================================================================
        //=================================================================

        #region -- Update Product --
        public SingleRsp UpdateProduct(Products pro)
        {
            var res = new SingleRsp();
            using (var context = new EatWellDBContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var t = context.Products.Update(pro);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }

        #endregion
        //=================================================================
        //=================================================================

        #region -- Delete Product --
        public bool DeleteProduct(int id)
        {
            EatWellDBContext db = new EatWellDBContext();
            Products product = db.Products.FirstOrDefault(x => x.ProductId == id);

            if (product == null) return false;
            
            // find options of product user want to remove in ProductOptions table
            // and delete those data record
            var options_of_product = db.ProductOptions.Where(x => x.ProductId == id).ToList();

            foreach (ProductOptions option_of_product in options_of_product)
            {
                // remove option of that product
                db.ProductOptions.Remove(option_of_product);
                db.SaveChanges();
            }

            // we need remove options of product before remove product
            // now we remove product user want
            db.Products.RemoveRange(product);
            
            db.SaveChangesAsync();
            return true;
        }
        #endregion
        //=================================================================
        //=================================================================

        #region -- Search Product --
        public object searchProductWithPagination(string key, int page, int size)
        {
            List<object> res = new List<object>();
            var cmn = (SqlConnection)Context.Database.GetDbConnection();
            if (cmn.State == ConnectionState.Closed)
                cmn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmt = cmn.CreateCommand();
                cmt.CommandText = "spSel_SearchProduct";
                cmt.CommandType = CommandType.StoredProcedure;
                cmt.Parameters.AddWithValue("@key", key);
                cmt.Parameters.AddWithValue("@page", page);
                cmt.Parameters.AddWithValue("@size", size);
                da.SelectCommand = cmt;
                da.Fill(ds);
                //kiem tra
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            ProductName = row["ProductName"],
                            CategoryName = row["CategoryName"],
                            Description = row["Description"],
                            Photo = row["Photo"],
                            IsActive = row["IsActive"],
                            ProductSlug = row["ProductSlug"],
                        };
                        res.Add(x);
                    }
                }
            }
            catch (Exception e)
            {
                res = null;
            }
            return res;
        }
        #endregion
    }
}