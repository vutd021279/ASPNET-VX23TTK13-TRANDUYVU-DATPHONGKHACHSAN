// DAL/RoomTypeDAL.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HotelBooking.Models;

namespace HotelBooking.DAL
{
    public class RoomTypeDAL
    {
        public List<RoomType> GetAllRoomTypes()
        {
            List<RoomType> roomTypes = new List<RoomType>();
            string query = "SELECT * FROM RoomTypes ORDER BY Price";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                roomTypes.Add(new RoomType
                {
                    RoomTypeID = Convert.ToInt32(row["RoomTypeID"]),
                    TypeName = row["TypeName"].ToString(),
                    Description = row["Description"].ToString(),
                    Price = Convert.ToDecimal(row["Price"]),
                    Capacity = Convert.ToInt32(row["Capacity"]),
                    ImageUrl = row["ImageUrl"].ToString(),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                });
            }
            return roomTypes;
        }

        public RoomType GetRoomTypeByID(int id)
        {
            string query = "SELECT * FROM RoomTypes WHERE RoomTypeID = @ID";
            SqlParameter[] parameters = { new SqlParameter("@ID", id) };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new RoomType
                {
                    RoomTypeID = Convert.ToInt32(row["RoomTypeID"]),
                    TypeName = row["TypeName"].ToString(),
                    Description = row["Description"].ToString(),
                    Price = Convert.ToDecimal(row["Price"]),
                    Capacity = Convert.ToInt32(row["Capacity"]),
                    ImageUrl = row["ImageUrl"].ToString(),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                };
            }
            return null;
        }

        public int GetAvailableRoomCount(int roomTypeID)
        {
            string query = @"SELECT COUNT(*) FROM Rooms 
                           WHERE RoomTypeID = @RoomTypeID AND Status = 'Available'";
            SqlParameter[] parameters = { new SqlParameter("@RoomTypeID", roomTypeID) };
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, parameters));
        }

        public bool Insert(RoomType roomType)
        {
            string query = @"INSERT INTO RoomTypes (TypeName, Description, Price, Capacity, ImageUrl) 
                           VALUES (@TypeName, @Description, @Price, @Capacity, @ImageUrl)";
            SqlParameter[] parameters = {
                new SqlParameter("@TypeName", roomType.TypeName),
                new SqlParameter("@Description", roomType.Description ?? ""),
                new SqlParameter("@Price", roomType.Price),
                new SqlParameter("@Capacity", roomType.Capacity),
                new SqlParameter("@ImageUrl", roomType.ImageUrl ?? "")
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool Update(RoomType roomType)
        {
            string query = @"UPDATE RoomTypes SET TypeName = @TypeName, Description = @Description, 
                           Price = @Price, Capacity = @Capacity, ImageUrl = @ImageUrl 
                           WHERE RoomTypeID = @ID";
            SqlParameter[] parameters = {
                new SqlParameter("@ID", roomType.RoomTypeID),
                new SqlParameter("@TypeName", roomType.TypeName),
                new SqlParameter("@Description", roomType.Description ?? ""),
                new SqlParameter("@Price", roomType.Price),
                new SqlParameter("@Capacity", roomType.Capacity),
                new SqlParameter("@ImageUrl", roomType.ImageUrl ?? "")
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool Delete(int id)
        {
            string query = "DELETE FROM RoomTypes WHERE RoomTypeID = @ID";
            SqlParameter[] parameters = { new SqlParameter("@ID", id) };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}