using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ICWR.Data
{
    public class MISMasterData
    {
        SqlParameter[] Param(MISMasterDto obj, int flag)
        {

            SqlParameter[] Param = new SqlParameter[3];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@Name", SqlDbType.NVarChar);
            Param[1].Value = obj.Name;

            Param[2] = new SqlParameter("@flag", SqlDbType.BigInt);
            Param[2].Value = flag;






            return Param;
        }

        public DataSet getFinYear()
        {
            MISMasterDto obj = new MISMasterDto();
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMISMaster", Params);

                    if (result != null && result.Tables[0].Rows.Count > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {

                }
            }
            return null;
        }

        public DataSet getFinYearForRunRate()
        {
            MISMasterDto obj = new MISMasterDto();
            var Params = Param(obj, 7);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMISMaster", Params);

                    if (result != null && result.Tables[0].Rows.Count > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {

                }
            }
            return null;
        }

        public DataSet getDivision()
        {
            MISMasterDto obj = new MISMasterDto();
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMISMaster", Params);

                    if (result != null && result.Tables[0].Rows.Count > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {

                }
            }
            return null;
        }

        public DataSet getDivisionWithUniqueID()
        {
            MISMasterDto obj = new MISMasterDto();
            var Params = Param(obj, 8);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMISMaster", Params);

                    if (result != null && result.Tables[0].Rows.Count > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {

                }
            }
            return null;
        }

        public DataSet getType()
        {
            MISMasterDto obj = new MISMasterDto();
            var Params = Param(obj, 3);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMISMaster", Params);

                    if (result != null && result.Tables[0].Rows.Count > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {

                }
            }
            return null;
        }

        public DataSet getAllType()
        {
            MISMasterDto obj = new MISMasterDto();
            var Params = Param(obj, 6);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMISMaster", Params);

                    if (result != null && result.Tables[0].Rows.Count > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {

                }
            }
            return null;
        }

        public DataSet getSubDivisionResults(string DivisionID)
        {
            MISMasterDto obj = new MISMasterDto();
            obj.ID = DivisionID.ToLong();
            var Params = Param(obj, 4);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMISMaster", Params);

                    if (result != null && result.Tables[0].Rows.Count > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {

                }
            }
            return null;
        }

        public DataSet FYearWithMonthName()
        {
            MISMasterDto obj = new MISMasterDto();
            var Params = Param(obj, 5);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMISMaster", Params);

                    if (result != null && result.Tables[0].Rows.Count > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {

                }
            }
            return null;
        }

        public DataSet getSubDivisionResultsWithUniqueKey(string DivisionID)
        {
            MISMasterDto obj = new MISMasterDto();
            obj.Name = DivisionID;
            var Params = Param(obj, 9);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMISMaster", Params);

                    if (result != null && result.Tables[0].Rows.Count > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {

                }
            }
            return null;
        }
    }
}